
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StateEnemyEnd : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StateEnemyEnd(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      _battleManager.ChangePlayerStartState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
      
    }

  }
}