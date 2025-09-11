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
      // 1. 적 행동
      
      
      // 2. 플레이어 턴으로 변경
      _battleManager.ChangePlayerTurnState();
    }

    public void Execute()
    {
      
    }

    public void Exit()
    {
      
    }
  }
}