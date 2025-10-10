using Core;
using Core.Event;

namespace GamePlay.Battle.State
{
  public class TurnEndState : IBattleState
  {
    private BattleManager _manager;
    private StateMachine _fsm;

    private TurnOwner _turnOwner;

    public TurnEndState(BattleManager manager, StateMachine fsm, TurnOwner owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public void Enter()
    {
      UnityEngine.Debug.Log($"--- {_turnOwner}의 턴 종료! ---");
      var team = _turnOwner == TurnOwner.PlayerTeam ? _manager.PlayerTeam : _manager.EnemyTeam;
      BattleEvent.RaiseTurnEnd(team);

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