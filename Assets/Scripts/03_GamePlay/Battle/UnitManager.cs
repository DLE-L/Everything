using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Collectible.Card;
using Data.Units;
using GamePlay.Battle.State;
using GamePlay.Units;
using UIs.Battle;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace GamePlay.Battle
{
  public class UnitManager : MonoBehaviour
  {
    private Player _player => GameSystem.Instance.Run.Player;
    public StatData PlayerStat => _player.Stat;
    public List<Unit> PlayerTeam { get; private set; } = new();
    public List<Unit> EnemyTeam { get; private set; } = new();
    
    [SerializeField] private AssetReference _enemyPrefabRef;
    [SerializeField] private List<Transform> _enemiesTransform;
    private EncounterCombat _combatEncounter;
    private BattleManager _battleManager;

    private void Awake()
    {
      PlayerTeam = new List<Unit>();
      EnemyTeam = new List<Unit>();
    }

    public async Task Init(BattleManager manager)
    {
      _battleManager = manager;
      _battleManager.currentCombat = GameSystem.Instance.CurrentEncounter as EncounterCombat;
      await SpawnEnemiesAsync(_battleManager.currentCombat);
      SubscribeToUnitDeath();
      
      GameSystem.Instance.CurrentEncounter = null;
    }

    private void Start()
    {
      PlayerTeam = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Player>() as Unit).ToList();
    }

    private async Task SpawnEnemiesAsync(EncounterSO encounter)
    {
      _combatEncounter = encounter as EncounterCombat;
      if (_combatEncounter is null) return;

      _enemiesTransform = new List<Transform>(FindAnyObjectByType<Canvas_Scene_Battle>().enemiesTransform);
      if (_enemiesTransform is null)
      {
        Debug.LogError($"SpawnEnemiesAsync: cannot find enemiesTransform");
        return;
      }
      
      List<Task<GameObject>> spawnTasks = new();
      for (var index = 0; index < _combatEncounter.Enemies.Count; index++)
      {        
        var spawnPosition = _enemiesTransform[index].position;
        var spawnTask = AssetLoader.InstantiateAsync(_enemyPrefabRef, spawnPosition, Quaternion.identity, _enemiesTransform[index]);
        spawnTasks.Add(spawnTask);
      }

      GameObject[] enemyInstances = await Task.WhenAll(spawnTasks);
      for (var index = 0; index < enemyInstances.Length; index++)
      {
        var enemyInstance = enemyInstances[index];
        var enemySo = _combatEncounter.Enemies[index];
        var controller = enemyInstance.GetComponent<EnemyController>();

        controller.DataSetting(enemySo, _battleManager);
        EnemyTeam.Add(controller);
      }
    }

    private void OnUnitDeath(Unit deadUnit)
    {
      deadUnit.OnDeath -= OnUnitDeath;
      if (deadUnit is EnemyController)
      {
        var deadUnitGO = deadUnit.gameObject;
        AssetLoader.ReleaseInstance(deadUnitGO);
        EnemyTeam.Remove(deadUnit);
        BattleEvent.RaiseEnemyKill(deadUnit);
        if (EnemyTeam.Count is not 0) return;
        
        UnsubscribeToUnitDeath();
        _battleManager.Fsm.ChangeState(new StateWin(_battleManager, _battleManager.Fsm));
      }

      if (deadUnit is not Player) return;
      
      UnsubscribeToUnitDeath();
      _battleManager.Fsm.ChangeState(new StateLose(_battleManager, _battleManager.Fsm));
    }

    private void SubscribeToUnitDeath()
    {
      _player.OnDeath += OnUnitDeath;
      foreach (var unit in EnemyTeam)
      {
        var enemy = (EnemyController)unit;
        enemy.OnDeath += OnUnitDeath;
      }
    }

    private void UnsubscribeToUnitDeath()
    {
      _player.OnDeath -= OnUnitDeath;
      foreach (var unit in EnemyTeam)
      {
        var enemy = (EnemyController)unit;
        enemy.OnDeath -= OnUnitDeath;
      }
    }
  }
  
  public enum TurnOwner
  {
    PlayerTeam,
    EnemyTeam
  }
}