using Units;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class SetupBattle : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;

    public SetupBattle(BattleManager manager, BattleFSM fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }

    public void Enter()
    {
      // 1. 플레이어 덱 로드
      _manager.GetPlayerDeck();
      // 3. Setup 상태 종료(플레이어 턴 상태로 변경)
      _fsm.ChangeState(new TurnPlayerState(_manager, _fsm));
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
  }
}