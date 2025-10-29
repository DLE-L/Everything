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
      //UnityEngine.Debug.Log($"-{_turnOwner}의 턴 시작!-");      
      BattleEvent.RaiseTurnStart(_turnOwner);

      _fsm.ChangeState(GetNextTurn(_turnOwner));
    }

    public void Execute() { }
    public void Exit() { }

    private IBattleState GetNextTurn(TurnOwner owner)
    {
      return owner switch
      {
        TurnOwner.EnemyTeam => new StateTurnEnemy(_manager, _fsm),
        TurnOwner.PlayerTeam => new StateTurnPlayer(_manager, _fsm),
        _ => null
      };
    }
  }
}