using Core;
using Core.Event;

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
    }

    public void Execute()
    {
      throw new System.NotImplementedException();
    }

    public void Exit()
    {
      throw new System.NotImplementedException();
    }
  }
}