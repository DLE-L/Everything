using Units;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class SetupBattle : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;
    private Unit _player;

    public SetupBattle(BattleManager manager, BattleFSM fsm, Unit player)
    {
      _manager = manager;
      _fsm = fsm;
      _player = player;
    }

    public void Enter()
    {
      BattleEvent.RaiseCombatStart();
      _fsm.ChangeState(new TurnStartState(_manager, _fsm, _player));
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
  }
}