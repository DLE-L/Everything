
using System.Collections.Generic;
using Units.Player;
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
      // 1. 카드 5장 드로우
      _battleManager.DrawCard(5);
      _battleManager.ResetEnergy();

      Execute();
    }

    public void Execute()
    {
      // 1. 플레이어 행동

      // 2. 연결된 버튼 클릭해 턴 종료
    }

    public void Exit()
    {
      _battleManager.DiscardHandCardAll();
      _battleManager.ResetBlock();
    }
  }
}