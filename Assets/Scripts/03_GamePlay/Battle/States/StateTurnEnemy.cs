using Core;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateTurnEnemy : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;

    public StateTurnEnemy(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    public void Enter()
    {
      Debug.Log($"-Enemy Turn-");

      foreach (var unit in _manager.UnitManager.EnemyTeam)
      {
        // 카드 사용 로직
      }

      _fsm.ChangeState(new StateTurnEnd(_manager, _fsm, TurnOwner.EnemyTeam));
    }

    public void Execute()
    {

      // 3. 플레이어 턴으로 변경
      // _battleManager.ChangePlayerTurnState();
    }

    public void Exit()
    {

    }
  }
}