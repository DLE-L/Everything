using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Act.Encounter;
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
    public Player PlayerUnit { get; private set; }
    public StatData PlayerStat => PlayerUnit.Stat;
    public List<Unit> PlayerTeam { get; private set; } = new();
    public List<Unit> EnemyTeam { get; private set; } = new();
    
    private BattleManager _battleManager;

    private void Awake()
    {
      PlayerUnit ??= FindAnyObjectByType<Player>();
      PlayerTeam = new List<Unit>();
      EnemyTeam = new List<Unit>();
    }

    public async Task Init()
    {
      _battleManager = GameSystem.Instance.Battle;
      var currentCombat = GameSystem.Instance.Run.CurrentEncounter as EncounterCombat;
      _battleManager.currentCombat = currentCombat;
      await SpawnEnemiesAsync(currentCombat,
        _battleManager.AssetLoader.EnemyPrefabRef,
        new List<Transform>(FindAnyObjectByType<Battle_Canvas>().EnemiesTransform));

      SubscribeToUnitDeath();
    }

    public void Cleanup()
    {
      UnsubscribeToUnitDeath();
      
      EnemyTeam.Clear();
      PlayerTeam.Clear();
    }

    private void Start()
    {
      PlayerTeam = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Player>() as Unit).ToList();
    }

    private async Task SpawnEnemiesAsync(EncounterCombat encounterCombat, AssetReference enemyPrefabRef, List<Transform> enemiesTrs)
    {
      List<Task<GameObject>> spawnTasks = new();
      for (var index = 0; index < encounterCombat.Enemies.Count; index++)
      {        
        var spawnPosition = enemiesTrs[index].position;
        var spawnTask = AssetLoader.InstantiateAsync(enemyPrefabRef, spawnPosition, Quaternion.identity, enemiesTrs[index]);
        spawnTasks.Add(spawnTask);
      }

      GameObject[] enemyInstances = await Task.WhenAll(spawnTasks);
      for (var index = 0; index < enemyInstances.Length; index++)
      {
        var enemyInstance = enemyInstances[index];
        var enemySo = encounterCombat.Enemies[index];
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
        EnemyTeam.Remove(deadUnit);
        AssetLoader.ReleaseInstance(deadUnit.gameObject);
        
        if (EnemyTeam.Count is not 0) return;
        
        _battleManager.Fsm.ChangeState(new StateVictory(_battleManager, _battleManager.Fsm));
      }

      if (deadUnit is not Player) return;
      
      _battleManager.Fsm.ChangeState(new StateDefeat(_battleManager, _battleManager.Fsm));
    }

    private void SubscribeToUnitDeath()
    {
      PlayerUnit.OnDeath += OnUnitDeath;
      
      EnemyTeam.ForEach(unit =>  unit.OnDeath += OnUnitDeath);
    }

    private void UnsubscribeToUnitDeath()
    {
      PlayerUnit.OnDeath -= OnUnitDeath;
      
      EnemyTeam.ForEach(unit => unit.OnDeath -= OnUnitDeath);
    }
  }
  
  public enum TurnOwner
  {
    PlayerTeam,
    EnemyTeam
  }
}