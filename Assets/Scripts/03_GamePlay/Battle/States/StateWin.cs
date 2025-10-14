using Core;
using Core.Event;

namespace GamePlay.Battle.State
{
  public class StateWin : IBattleState
  {
    private BattleManager _manager;
    private StateMachine _fsm;
    public StateWin(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    
    public void Enter()
    {
      
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