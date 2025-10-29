using Core;
using Core.Event;
using Data.Act.Encounter;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateVictory : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;
    public StateVictory(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    
    public void Enter()
    {
      Debug.Log($"Player Victory");
      _fsm.ChangeState(new StateCleanupBattle(_manager, _fsm, BattleResult.Victory));
    }

    public void Execute() { }
    public void Exit() { }
  }
}