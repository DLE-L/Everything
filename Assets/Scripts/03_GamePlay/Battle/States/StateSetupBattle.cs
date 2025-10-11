using Core;
using Core.Event;

namespace GamePlay.Battle.State
{
  public class StateSetupBattle : IBattleState
  {
    private BattleManager _manager;
    private StateMachine _fsm;
    private TurnOwner _turnOwner;

    public StateSetupBattle(BattleManager manager, StateMachine fsm, TurnOwner owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public void Enter()
    {
      BattleEvent.RaiseCombatStart();
      _fsm.ChangeState(new StateTurnStart(_manager, _fsm, _turnOwner));
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
  }
}