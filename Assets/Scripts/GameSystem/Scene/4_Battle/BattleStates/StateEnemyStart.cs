
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StateEnemyStart : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StateEnemyStart(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      _battleManager.ChangeEnemyTurnState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
      
    }
  }
}