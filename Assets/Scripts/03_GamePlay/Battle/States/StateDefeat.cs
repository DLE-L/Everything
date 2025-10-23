using Core;
using Core.Event;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateDefeat : IBattleState
  {
    private BattleManager _manager;
    private StateMachine _fsm;
    public StateDefeat(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    public void Enter()
    {
      Debug.Log($"Player Defeat");
      
      _fsm.ChangeState(new StateCleanupBattle(_manager, _fsm, BattleResult.Defeat));
    }

    public void Execute()
    {
      
    }

    public void Exit()
    {
      
    }
  }
}