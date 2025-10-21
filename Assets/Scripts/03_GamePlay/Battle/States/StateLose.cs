using Core;
using Core.Event;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateLose : IBattleState
  {
    private BattleManager _manager;
    private StateMachine _fsm;
    public StateLose(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    public void Enter()
    {
      BattleEvent.RaisePlayerLose();
      SystemEvent.RaiseEndRun();
      Debug.Log($"Player Lose");
    }

    public void Execute()
    {
      
    }

    public void Exit()
    {
      
    }
  }
}