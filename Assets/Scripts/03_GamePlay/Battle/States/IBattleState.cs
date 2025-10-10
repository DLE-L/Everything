namespace GamePlay.Battle.State
{
  public interface IBattleState
  {
    public void Enter();
    public void Execute();
    public void Exit();
  }
}