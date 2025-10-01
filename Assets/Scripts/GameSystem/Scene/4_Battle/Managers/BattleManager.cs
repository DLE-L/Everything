using UnityEngine;
using System.Collections.Generic;
using GameSystems.Scene.Game;
using Utils;
using Item;
using System.Linq;
using Units;
using System;
using System.Threading.Tasks;

namespace GameSystems.Scene.Battle
{
  public class BattleManager : MonoBehaviour
  {
    public RunPlayer Player => GameSystem.Instance.Player;
    public StatData PlayerStat => Player.Stat;

    public List<Unit> PlayerTeam { get; private set; } = new();
    public List<Unit> EnemyTeam { get; private set; } = new();
    public DeckSO PlayerStartDeck;
    public BattleFSM FSM { get; private set; } = new();
    public CardManager CardManager { get; private set; }

    public System.Random random = new();

    [SerializeField] private GameObject enemyGameObject;
    [SerializeField] private Transform enemyTransform;

    public event Action<Unit> OnEnemyClicked;

    private TaskCompletionSource<Unit> _currentTargetSelectionTask;

    private void Awake()
    {
      GameSystem.Instance.RegisterBattleManager(this);

      PlayerTeam = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<RunPlayer>() as Unit).ToList();      
    }

    private void Start()
    {     
      List<CardSO> DeckList = PlayerStartDeck.Cards
                              .SelectMany(cardCount => Enumerable.Repeat(cardCount.Card, cardCount.Count))
                              .ToList();
      CardManager = new CardManager(DeckList);



      

      FSM.ChangeState(new SetupBattle(this, FSM, TurnOwner.Player));

      // 1. 배틀 입장 - 게임 씬에서 적 정보 획득 및 생성
      BattleEncounter();

      // 2. 배틀 첫 상태 시작
      // 3. 플레이어 & 적 이벤트 등록
      Player.OnDeath += OnUnitDied;
      foreach (EnemyController enemy in EnemyTeam)
      {
        enemy.OnDeath += OnUnitDied;
      }


      EnemyNextCard();

      
    }

    public void Update()
    {
      FSM.Execute();

      HandlePlayerClick();
    }

    public void BattleEncounter()
    {
      // TODO: 인타운터 AssetReferenceT로 변경으로 인해 재구현 필요
      var encounter = EncounterDatabase.CurrentEncounter;
      int count = 0;
      foreach (var enemy in encounter.Enemies)
      {
        enemyTransform.position += new Vector3(0, count, 0);
        GameObject go = Instantiate(enemyGameObject, enemyTransform);
        EnemyController controller = go.GetComponent<EnemyController>();
        controller.EnemyData = new BattleEnemyData(enemy);
        go.name = enemy.name + count;
        controller.Init();
        EnemyTeam.Add(controller);
        count++;
      }
    }

    public void EnemyNextCard()
    {
      for (int i = 0; i < EnemyTeam.Count; i++)
      {
        EnemyController enmey = EnemyTeam[i] as EnemyController;
        int rand = random.Next(0, enmey.EnemyData.AbilityCards.Count);
        CardSO card = enmey.EnemyData.AbilityCards[rand];
        Debug.Log($"[{enmey.name}_Next Card]:{card.name}");
      }
    }

    public void OnUnitDied(Unit unit)
    {
      if (unit is EnemyController)
      {
        EnemyTeam.Remove(unit);
        if (EnemyTeam.Count == 0)
        {
          Debug.Log("[플레이어 승리]");
          // ChangeState(new WinState(this, StateSystem));
        }
        else if (EnemyTeam.Count > 0)
        {
          Debug.Log($"[Death Enemy: {unit.name}]");
        }
      }
      else if (unit is RunPlayer)
      {
        BattleEvent.RaiseCombatEnd();
        Debug.Log("[적 승리]");
        // ChangeState(new LoseState(this, StateSystem));        
      }
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
        if (hit.collider != null)
        {
          Unit enemy = hit.collider.GetComponent<Unit>();
          if (enemy != null)
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
    }

    public bool UseEnergy(int cost)
    {
      if (PlayerStat.Energy < cost)
      {
        return false;
      }
      PlayerStat.Energy -= cost;
      //Debug.Log($"남은 에너지: {PlayerStat.Energy}");
      return true;
    }

    public void ResetBlock(Unit unit)
    {
      if (unit is RunPlayer)
      {
        PlayerStat.Block = 0;
      }
      else if (unit is EnemyController)
      {
        foreach (EnemyController enemy in EnemyTeam)
        {
          enemy.Stat.Block = 0;
        }
      }
    }
    public void ResetEnergy(Unit unit)
    {
      if (unit is RunPlayer)
      {
        PlayerStat.Energy = PlayerStat.MaxEnergy;
      }
      else if (unit is EnemyController)
      {
        foreach (EnemyController enemy in EnemyTeam)
        {
          enemy.Stat.Energy = enemy.Stat.MaxEnergy;
        }
      }
    }

    public void CardUIUpdate(UI_Card_Battle card, bool active)
    {
      card.UpdateUI();
      card.gameObject.SetActive(active);
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
    Player,
    Enemy
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