using Core;
using Core.Event;

namespace GamePlay.Battle.State
{
  public class StateTurnStart : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;
    private readonly TurnOwner _turnOwner;

    public StateTurnStart(BattleManager manager, StateMachine fsm, TurnOwner owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public void Enter()
    {
      UnityEngine.Debug.Log($"-{_turnOwner}의 턴 시작!-");      
      BattleEvent.RaiseTurnStart(_turnOwner);

      if (_turnOwner == TurnOwner.PlayerTeam)
      {
        _fsm.ChangeState(new StateTurnPlayer(_manager, _fsm));
      }
      else if (_turnOwner == TurnOwner.EnemyTeam)
      {
        _fsm.ChangeState(new StateTurnEnemy(_manager, _fsm));
      }
    }

    public void Execute() { }
    public void Exit() { }
  }
}