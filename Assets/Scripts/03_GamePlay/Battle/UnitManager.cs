using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Act.Encounter;
using Data.Collectible.Card;
using Data.Units;
using GamePlay.Battle.State;
using GamePlay.Units;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace GamePlay.Battle
{
  public class UnitManager : MonoBehaviour
  {
    public Player Player => GameSystem.Instance.Player;
    public StatData PlayerStat => Player.Stat;
    public DeckSO PlayerStartDeck;
    public List<Unit> PlayerTeam { get; private set; } = new();
    public List<Unit> EnemyTeam { get; private set; } = new();
    
    [SerializeField] private AssetReference _enemyPrefabRef;
    [SerializeField] private Transform _enemyTransform;
    private EncounterCombat _combatEncounter;
    private BattleManager _battleManager;

    public async Task Init(BattleManager manager)
    {
      _battleManager = manager;
      _battleManager.currentBattleEncounter = GameSystem.Instance.CurrentEncounter;
      await SpawnEnemiesAsync(_battleManager.currentBattleEncounter);
      SubscribeToUnitDeath();
      
      GameSystem.Instance.CurrentEncounter = null;
    }

    private void Start()
    {
      PlayerTeam = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Player>() as Unit).ToList();
    }

    private async Task SpawnEnemiesAsync(EncounterSO encounter)
    {
      int count = 0;
      _combatEncounter = encounter as EncounterCombat;
      if (_combatEncounter is null) return;

      List<Task<GameObject>> spawnTasks = new();
      foreach (var enemySo in _combatEncounter.Enemies)
      {
        Vector3 spawnPosition = _enemyTransform.position + new Vector3(0, count, 0);
        var spawnTask = AssetLoader.InstantiateAsync(_enemyPrefabRef, spawnPosition, Quaternion.identity);
        spawnTasks.Add(spawnTask);
        count++;
      }

      GameObject[] enemyInstances = await Task.WhenAll(spawnTasks);

      for (int i = 0; i < enemyInstances.Length; i++)
      {
        GameObject enemyInstance = enemyInstances[i];
        EnemySO enemySo = _combatEncounter.Enemies[i];
        EnemyController controller = enemyInstance.GetComponent<EnemyController>();

        controller.DataSetting(new BattleEnemyData(enemySo), _battleManager);
        enemyInstance.name = enemySo.name;
        EnemyTeam.Add(controller);
      }
    }

    private void OnUnitDeath(Unit deadUnit)
    {
      GameObject deadUnitGO = deadUnit.gameObject;
      AssetLoader.ReleaseInstance(deadUnitGO);
      
      deadUnit.OnDeath -= OnUnitDeath;
      if (deadUnit is EnemyController)
      {
        EnemyTeam.Remove(deadUnit);
        
        if (EnemyTeam.Count == 0)
        {
          UnsubscribeToUnitDeath();
          _battleManager.Fsm.ChangeState(new StateWin(_battleManager, _battleManager.Fsm));
        }
      }
      else if (deadUnit is Player)
      {
        UnsubscribeToUnitDeath();
        
        _battleManager.Fsm.ChangeState(new StateLose(_battleManager, _battleManager.Fsm));
      }
    }

    private void SubscribeToUnitDeath()
    {
      Player.OnDeath += OnUnitDeath;
      foreach (var unit in EnemyTeam)
      {
        var enemy = (EnemyController)unit;
        enemy.OnDeath += OnUnitDeath;
      }
    }

    private void UnsubscribeToUnitDeath()
    {
      Player.OnDeath -= OnUnitDeath;
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