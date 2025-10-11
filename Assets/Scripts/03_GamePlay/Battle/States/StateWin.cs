using Core.Event;

namespace GamePlay.Battle.State
{
  public class StateWin : IBattleState
  {
    public void Enter()
    {
      BattleEvent.RaisePlayerWin();
      throw new System.NotImplementedException();
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