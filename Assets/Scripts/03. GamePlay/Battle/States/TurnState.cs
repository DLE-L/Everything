using System.Collections.Generic;
using GamePlay.Character;

namespace GamePlay.Battle.State
{
  public abstract class TurnState : IBattleState
  {
    public Unit user;
    public Unit target;
    public List<Unit> allAllies;
    public List<Unit> allEnemies;

    public void OnTurnStart()
    {

    }
    public void Enter()
    {
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