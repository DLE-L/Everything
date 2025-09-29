using Units;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class SetupBattle : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;
    private TurnOwner _turnOwner;

    public SetupBattle(BattleManager manager, BattleFSM fsm, TurnOwner owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public void Enter()
    {
      BattleEvent.RaiseCombatStart();
      _fsm.ChangeState(new TurnStartState(_manager, _fsm, _turnOwner));
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
  }
}