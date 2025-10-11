using Core.Event;

namespace GamePlay.Battle.State
{
  public class StateLose : IBattleState
  {
    public void Enter()
    {
      BattleEvent.RaisePlayerLose();
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