using Utils;

namespace GameSystems.Scene.Battle
{
  public class TurnStartState : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;

    private TurnOwner _turnOwner;

    public TurnStartState(BattleManager manager, BattleFSM fsm, TurnOwner owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public void Enter()
    {
      UnityEngine.Debug.Log($"--- {_turnOwner}의 턴 시작! ---");      
      BattleEvent.RaiseTurnStart(_turnOwner);

      if (_turnOwner == TurnOwner.PlayerTeam)
      {
        _fsm.ChangeState(new TurnPlayerState(_manager, _fsm));
      }
      else if (_turnOwner == TurnOwner.EnemyTeam)
      {
        _fsm.ChangeState(new TurnEnemyState(_manager, _fsm));
      }
    }

    public void Execute() { }
    public void Exit() { }
  }
}