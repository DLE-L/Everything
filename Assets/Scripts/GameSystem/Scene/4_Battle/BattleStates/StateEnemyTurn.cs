
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StateEnemyTurn : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StateEnemyTurn(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      
      _battleManager.ChangeEnemyEndState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
      
    }
  }
}