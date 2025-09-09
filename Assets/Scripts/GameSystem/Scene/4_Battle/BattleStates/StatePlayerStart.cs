
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StatePlayerStart : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StatePlayerStart(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }


    public void Enter()
    {
      // 1. 카드 5장 드로우
      _battleManager.DrawCard(5);
      _battleManager.ResetEnergy();

      // 2. 플레이어 턴 상태로 변경
      //_battleManager.ChangePlayerTurnState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
      
    }
  }
}