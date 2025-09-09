
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StatePlayerTurn : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StatePlayerTurn(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      Execute();
    }

    public void Execute()
    {
      // 1. 플레이어 행동

      // 2. 연결된 버튼 클릭해 턴 종료
    }

    public void Exit()
    {
     
    }
  }
}