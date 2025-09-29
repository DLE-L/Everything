using Units;
using Units.Enemy;
using Utils;
using Item;
using UnityEngine;

namespace GameSystems.Scene.Battle
{
  public class TurnEnemyState : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;

    public TurnEnemyState(BattleManager manager, BattleFSM fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    public void Enter()
    {
      Debug.Log($"[Enemy Turn State]");

      foreach (var unit in _manager.EnemyTeam)
      {
        // 카드 사용 로직
      }

      _fsm.ChangeState(new TurnEndState(_manager, _fsm, TurnOwner.Player));
    }

    public void Execute()
    {

      // 3. 플레이어 턴으로 변경
      // _battleManager.ChangePlayerTurnState();
    }

    public void Exit()
    {
      EnemyNextCard();
      _manager.ResetBlock(_manager.Player);
    }

    public void EnemyNextCard()
    {
      for (int i = 0; i < _manager.EnemyTeam.Count; i++)
      {
        var random = new System.Random();

        EnemyController enmey = _manager.EnemyTeam[i] as EnemyController;
        int rand = random.Next(0, enmey.EnemyData.AbilityCards.Count);
        CardSO card = enmey.EnemyData.AbilityCards[rand];
        UnityEngine.Debug.Log($"[{enmey.name}_Next Card]:{card.name}");
      }
    }
  }
}