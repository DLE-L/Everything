
using System.Collections.Generic;
using Units;
using Utils;

namespace GameSystems.Scene.Battle
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