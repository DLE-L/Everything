using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Core;
using Core.Event;
using GamePlay.Units;
using Data.Card;
using Data.Act.Encounter;
using Data.Units;
using GamePlay.Battle.State;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;
using StateMachine = Core.StateMachine;
using Unit = GamePlay.Units.Unit;

namespace GamePlay.Battle
{
  public class BattleManager : MonoBehaviour
  {
    #region Unit
    public Player Player => GameSystem.Instance.Player;
    private StatData PlayerStat => Player.Stat;
    public DeckSO PlayerStartDeck;
    public List<Unit> PlayerTeam { get; private set; } = new();
    public List<Unit> EnemyTeam { get; private set; } = new();
    #endregion
    #region Enemy
    public event Action<Unit> OnEnemyClicked;
    [SerializeField] private AssetReference _enemyPrefabRef;
    [SerializeField] private Transform _enemyTransform;
    private EncounterCombat _combatEncounter;
    private readonly HashSet<GameObject> _spawnedInstances = new();
    #endregion
    
    public StateMachine Fsm { get; private set; } = new();
    public CardManager CardManager { get; private set; }

    private TaskCompletionSource<Unit> _currentTargetSelectionTask;
    private RaycastHit2D _hit;

    private void Awake()
    {
      GameSystem.Instance.RegisterBattleManager(this);

      PlayerTeam = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Player>() as Unit).ToList();
    }

    private async void Start()
    {
      List<CardSO> DeckList = PlayerStartDeck.Cards
        .SelectMany(cardCount => Enumerable.Repeat(cardCount.Card, cardCount.Count))
        .ToList();
      CardManager = new CardManager(DeckList);
      


      Fsm.ChangeState(new StateSetupBattle(this, Fsm, TurnOwner.PlayerTeam));

      // 1. 배틀 입장 - 게임 씬에서 적 정보 획득 및 생성
      SpawnEnemies(GameSystem.Instance.CurrentEncounter);

      // 2. 배틀 첫 상태 시작
      // 3. 플레이어 & 적 이벤트 등록
      SubscribeToUnitDeath();
    }

    public void Update()
    {
      Fsm.Execute();

      HandlePlayerClick();
    }

    #region EnemySpawn
    private async void SpawnEnemies(EncounterSO encounter)
    {
      _combatEncounter = encounter as EncounterCombat;
      if (_combatEncounter is null) return;

      var spawnTasks = new List<Task<GameObject>>();
      int count = 0;
      foreach (var enemySo in _combatEncounter.Enemies)
      {
        Vector3 spawnPosition = _enemyTransform.position + new Vector3(0, count, 0);
        var spawnTask = _enemyPrefabRef.InstantiateAsync(spawnPosition, Quaternion.identity).Task;
        spawnTasks.Add(spawnTask);
        count++;
      }
      
      GameObject[] enemyInstances = await Task.WhenAll(spawnTasks);

      for (int i = 0; i < enemyInstances.Length; i++)
      {
        GameObject enemyInstance = enemyInstances[i];
        EnemySO enemySo = _combatEncounter.Enemies[i];
        EnemyController controller = enemyInstance.GetComponent<EnemyController>();
        
        controller.DataSetting(new BattleEnemyData(enemySo), this);
        enemyInstance.name = enemySo.name;
        EnemyTeam.Add(controller);
        _spawnedInstances.Add(enemyInstance);
      }
    }

    private void OnUnitDeath(Unit deadUnit)
    {
      GameObject deadUnitGO = deadUnit.gameObject;
      if (_spawnedInstances.Contains(deadUnitGO))
      {
        Addressables.ReleaseInstance(deadUnitGO);
        _spawnedInstances.Remove(deadUnitGO);
      }
      
      deadUnit.OnDeath -= OnUnitDeath;
      if (deadUnit is EnemyController)
      {
        _enemyPrefabRef.ReleaseInstance(deadUnit.gameObject);
        EnemyTeam.Remove(deadUnit);
        
        if (EnemyTeam.Count == 0)
        {
          UnsubscribeToUnitDeath();
          Fsm.ChangeState(new StateWin());
        }
      }
      else if (deadUnit is Player)
      {
        UnsubscribeToUnitDeath();
        CleanupAllEnemies();
        Fsm.ChangeState(new StateLose());
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
    
    private void CleanupAllEnemies()
    {
      foreach (var instance in _spawnedInstances)
      {
        Addressables.ReleaseInstance(instance);
      }
      _spawnedInstances.Clear();
    }
    #endregion
    
    public Task<Unit> SelectTargetAsync()
    {
      // 1. 새로운 '약속 티켓'을 발행합니다.
      _currentTargetSelectionTask = new TaskCompletionSource<Unit>();

      // 여기에 "적을 선택하세요" 화살표 UI를 활성화하는 코드를 넣습니다.
      //TargetingArrow.Instance.Show();

      // 2. 약속 티켓(Task)을 즉시 반환합니다. 
      //    (호출한 쪽에서는 이 Task를 await하며 기다리게 됩니다)
      return _currentTargetSelectionTask.Task;
    }

    private void HandlePlayerClick()
    {
      // 타겟 선택 대기 상태가 아닐 때는 클릭을 무시
      if (_currentTargetSelectionTask == null || _currentTargetSelectionTask.Task.IsCompleted)
      {
        return;
      }

      if (Input.GetMouseButtonDown(0))
      {
        // ... Raycast 로직 ...
        var enemyCollider = _hit.collider;
        if (enemyCollider is not null)
        {
          // 클릭된 적 정보를 이벤트로 방송 (다른 용도를 위해 남겨둘 수 있음)
          var enemy = enemyCollider.GetComponent<Unit>();
          OnEnemyClicked?.Invoke(enemy);

          // 3. '약속 티켓'에 결과를 기록하여, await 하던 곳을 깨웁니다!
          _currentTargetSelectionTask.SetResult(enemy);

          // 화살표 UI 비활성화
          //TargetingArrow.Instance.Hide();
        }
      }
    }

    public bool TryUseEnergy(int cardCost)
    {
      if (PlayerStat.Energy < cardCost)
      {
        return false;
      }

      PlayerStat.Energy -= cardCost;
      //Debug.Log($"남은 에너지: {PlayerStat.Energy}");
      return true;
    }

    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
      
    }

    void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterBattleManager();
      }
    }
  }

  public enum TurnOwner
  {
    PlayerTeam,
    EnemyTeam
  }
}

/*
[전투 시작]
1. Setup
2. Player Turn
3. Enemy Turn

2~3반복
[전투 종료] - Win, Loose
3-1. Win : 게임씬으로 복귀
3-2. Loose : Lobby로 퇴장

*/