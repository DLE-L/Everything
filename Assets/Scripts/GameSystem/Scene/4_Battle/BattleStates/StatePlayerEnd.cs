
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StatePlayerEnd : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StatePlayerEnd(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      // 1. 플레이어 핸드 카드 버리기
      _battleManager.DiscardHandCardAll();
      _battleManager.ResetBlock();
      
     // _battleManager.ChangeEnemyStartState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
      
    }

  }
}