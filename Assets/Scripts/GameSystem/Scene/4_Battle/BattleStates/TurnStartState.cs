using Units;
using Units.Player;
using Units.Enemy;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class TurnStartState : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;

    private Unit _turnOwner;

    public TurnStartState(BattleManager manager, BattleFSM fsm, Unit owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public void Enter()
    {
      // Status Effect 처리      
      foreach(var user in _turnOwner.StatusEffect)
      {
        user.Key.ProcessTurnStartEffects();
      }

      UnityEngine.Debug.Log($"--- {_turnOwner.name}의 턴 시작! ---");
      if (_turnOwner is Player)
      {
        _fsm.ChangeState(new TurnPlayerState(_manager, _fsm));
      }
      else if (_turnOwner is EnemyController)
      {
        _fsm.ChangeState(new TurnEnemyState(_manager, _fsm));
      }
    }

    public void Execute() { }
    public void Exit() { }
  }
}