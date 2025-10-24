using Core;
using Core.Event;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateDefeat : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;
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