using Core;

namespace GamePlay.Battle.State
{
  public class StateTurnEnd : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;
    private readonly TurnOwner _turnOwner;

    public StateTurnEnd(BattleManager manager, StateMachine fsm, TurnOwner owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public void Enter()
    {
      //UnityEngine.Debug.Log($"-{_turnOwner} 턴 종료!-");
      var nextTurnOwner = _turnOwner is TurnOwner.PlayerTeam ? TurnOwner.EnemyTeam : TurnOwner.PlayerTeam;

      _fsm.ChangeState(new StateTurnStart(_manager, _fsm, nextTurnOwner));
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
  }
}