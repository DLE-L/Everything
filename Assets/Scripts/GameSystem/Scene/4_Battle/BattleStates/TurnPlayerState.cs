using Units.Enemy;
using UnityEngine;
using Units;
using Utils;
using Item;
using System.Collections.Generic;

namespace GameSystems.Scene.Battle
{
  public class TurnPlayerState : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;
    private Unit _playerUnit;
    private bool _isActionInProgress = false;

    public TurnPlayerState(BattleManager manager, BattleFSM fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }

    public void Enter()
    {
      Debug.Log($"[Player Turn State]");
      _manager.CardManager.TurnStartDiscardHand();

      _manager.CardManager.Draw(5);
      _manager.ResetEnergy(_manager.Player);
      
      // 외부에서 턴 넘김
    }

    public void Execute()
    {
      // 현재 다른 액션(카드 사용 등)이 진행 중이라면, 새로운 입력을 받지 않음
      if (_isActionInProgress) return;

      // BattleManager의 카드 클릭 이벤트를 여기서 사용하거나, 직접 Raycast 할 수 있음
      // 여기서는 BattleManager가 클릭된 카드를 알려준다고 가정
      // if (_manager.TryGetClickedCard(out BattleCard clickedCard))
      // {
      //   // 카드가 클릭되면, 카드 사용 프로세스를 비동기로 시작
      //   UseCardProcessAsync(clickedCard);
      // }
    }
    private async void UseCardProcessAsync(BattleCard battleCard)
    {
      _isActionInProgress = true; // 행동 시작, 다른 입력 잠금

      CardSO cardSO = battleCard.CardData;
      Unit user = _manager.Player;

      // 1. 타겟팅 전략에 따라 타겟을 결정 (플레이어 선택이 필요하면 여기서 대기)
      List<Unit> targets = new();//= await cardSO.Targeting.FindTargetsAsync(new TargetingContext());

      // 2. 타겟 선택이 완료되면 (취소되지 않았다면)
      if (targets != null && targets.Count > 0)
      {
        // 3. 카드 효과를 발동
        foreach (Unit target in targets)
        {          
          foreach (var effect in cardSO.Effects)
          {
            //effect.Execute(user, target);
          }
        }
        // 4. 카드 사용 후 처리 (버리기 등)
        _manager.CardManager.Discard(cardSO);        
      }

      _isActionInProgress = false; // 행동 종료, 다시 입력 가능
    }

    public void Exit()
    {

    }
  }
}