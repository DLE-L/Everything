using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Core;
using Data.Collectible.Card;
using Data.Act.Encounter;
using Data.Reward;
using GamePlay.Battle.State;
using GamePlay.Units;
using UIs.Battle;
using StateMachine = Core.StateMachine;
using Unit = GamePlay.Units.Unit;

namespace GamePlay.Battle
{
  public class BattleManager : MonoBehaviour
  {
    private TaskCompletionSource<Unit> _currentTargetSelectionTask;
    private RaycastHit2D _hit;
    private CardSO _selectedCard;
    public event Action<Unit> OnEnemyClicked;
    public EncounterCombat currentCombat;
    
    public StateMachine Fsm { get; private set; } = new();
    public CardManager CardManager { get; private set; }
    public UnitManager UnitManager { get; private set; }
    public BattleUIManager UIManager { get; private set; }

    private void Awake()
    {
      GameSystem.Instance.RegisterBattleManager(this);
      UnitManager ??= FindAnyObjectByType<UnitManager>();
      UIManager ??= FindAnyObjectByType<BattleUIManager>();
    }

    private async void Start()
    {
      try
      {
        var DeckList = GameSystem.Instance.Run.PlayerRunData.Deck
          .SelectMany(deck => Enumerable.Repeat(deck.Key, deck.Value))
          .ToList();
        CardManager = new CardManager(DeckList);
      
        // TODO: 적 의도 보여줌

        Fsm.ChangeState(new StateSetupBattle(this, Fsm, TurnOwner.PlayerTeam));
      }
      catch (Exception e)
      {
        Debug.LogError($"BattleManager Start warning: {e.Message}");
      }
    }

    public void Update()
    {
      Fsm.Execute();

      HandlePlayerClick();
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
      if (_currentTargetSelectionTask is null || _currentTargetSelectionTask.Task.IsCompleted)
      {
        return;
      }

      if (!Input.GetMouseButtonDown(0)) return;
      
      var hitCollider = _hit.collider;
      
      if (hitCollider is null) return;

      if (hitCollider.gameObject.CompareTag("Enemy"))
      {
        var enemy = hitCollider.GetComponent<EnemyController>();
        OnEnemyClicked?.Invoke(enemy);

        // 3. '약속 티켓'에 결과를 기록하여, await 하던 곳을 깨웁니다!
        _currentTargetSelectionTask.SetResult(enemy);  
      }
      else if (hitCollider.gameObject.CompareTag("Player"))
      {
        
      }
      

      // 화살표 UI 비활성화
      //TargetingArrow.Instance.Hide();
    }

    public bool TryUseEnergy(int cardCost)
    {
      if (UnitManager.PlayerStat.Energy < cardCost)
      {
        return false;
      }

      UnitManager.PlayerStat.Energy -= cardCost;
      //Debug.Log($"남은 에너지: {PlayerStat.Energy}");
      return true;
    }

    void OnDestroy()
    {
      if (GameSystem.Instance is not null)
      {
        GameSystem.Instance.UnregisterBattleManager();
      }
    }
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