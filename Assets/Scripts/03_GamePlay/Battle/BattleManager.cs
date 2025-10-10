using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Core;
using GamePlay.Units;
using Data.Card;
using Core.Event;
using Data.Units;
using GamePlay.Battle.State;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GamePlay.Battle
{
  public class BattleManager : MonoBehaviour
  {
    public Player Player => GameSystem.Instance.Player;
    private StatData PlayerStat => Player.Stat;

    public List<Unit> PlayerTeam { get; private set; } = new();
    public List<Unit> EnemyTeam { get; private set; } = new();
    public DeckSO PlayerStartDeck;
    public StateMachine FSM { get; private set; } = new();
    public CardManager CardManager { get; private set; }

    public System.Random random = new();

    [SerializeField] private AssetReference _enemyGameObjectRef;
    [SerializeField] private Transform _enemyTransform;
    
    private AsyncOperationHandle<GameObject> _currentGameObjectHandle;

    public event Action<Unit> OnEnemyClicked;

    private TaskCompletionSource<Unit> _currentTargetSelectionTask;

    private void Awake()
    {
      GameSystem.Instance.RegisterBattleManager(this);

      PlayerTeam = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Player>() as Unit).ToList();      
    }

    private void Start()
    {     
      List<CardSO> DeckList = PlayerStartDeck.Cards
                              .SelectMany(cardCount => Enumerable.Repeat(cardCount.Card, cardCount.Count))
                              .ToList();
      CardManager = new CardManager(DeckList);



      

      FSM.ChangeState(new SetupBattle(this, FSM, TurnOwner.PlayerTeam));

      // 1. 배틀 입장 - 게임 씬에서 적 정보 획득 및 생성
      BattleEncounter();

      // 2. 배틀 첫 상태 시작
      // 3. 플레이어 & 적 이벤트 등록
      Player.OnDeath += OnUnitDeath;
      
      foreach (EnemyController unit in EnemyTeam)
      {
        EnemyController enemy = unit;
        enemy.OnDeath  += OnUnitDeath;
      }
    }

    public void Update()
    {
      FSM.Execute();

      HandlePlayerClick();
    }

    public void BattleEncounter()
    {
      // TODO: 인타운터 AssetReferenceT로 변경으로 인해 재구현 필요
      
      // int count = 0;
      // foreach (var enemy in encounter.Enemies)
      // {
      //   _enemyTransform.position += new Vector3(0, count, 0);
      //   _currentGameObjectHandle = _enemyGameObjectRef.InstantiateAsync(_enemyTransform);
      //   GameObject enemyInstance = _currentGameObjectHandle.Result;
      //   EnemyController controller = enemyInstance.GetComponent<EnemyController>();
      //   controller.DataSetting(new BattleEnemyData(enemy), this);
      //   go.name = enemy.name + count;        
      //   EnemyTeam.Add(controller);
      //   count++;
      // }
    }

    public void OnUnitDeath(Unit unit)
    {
      if (unit is EnemyController)
      {
        EnemyTeam.Remove(unit);
        if (EnemyTeam.Count == 0)
        {
          Debug.Log("[Player Win]");
          // ChangeState(new WinState(this, StateSystem));
        }
        else if (EnemyTeam.Count > 0)
        {
          Debug.Log($"[Death Enemy: {unit.name}]");
        }
      }
      else if (unit is Player)
      {
        BattleEvent.RaiseCombatEnd();
        // 적 캐릭터 구독 모두 해제
        foreach (var enemy in EnemyTeam)
        {
          enemy.OnDeath -= OnUnitDeath;
        }
        Debug.Log("[Enemy Win]");
        // ChangeState(new LoseState(this, StateSystem));        
      }
      
      unit.OnDeath -= OnUnitDeath;
    }

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
        RaycastHit2D hit = new();
        Unit enemy = hit.collider?.GetComponent<Unit>();
        if (enemy is not null)
        {
          // 클릭된 적 정보를 이벤트로 방송 (다른 용도를 위해 남겨둘 수 있음)
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