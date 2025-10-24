using Core;
using Core.Event;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateCleanupBattle : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;
    private readonly BattleResult _result;

    public StateCleanupBattle(BattleManager manager, StateMachine fsm, BattleResult result)
    {
      _manager = manager;
      _fsm = fsm;
      _result = result;
    }

    public void Enter()
    {
      Debug.Log($"Battle Cleanup");
      _manager.UnitManager.Cleanup();
      if (_result is BattleResult.Defeat)
      {
        SystemEvent.RaiseEndRun();
        return;
      }

      BattleEvent.RaiseRewardPhaseStart(_manager.currentCombat.RewardStrategy);
      BattleEvent.RaiseCombatEnd();
    }

    public void Execute()
    {
      
    }

    public void Exit()
    {
      
    }
  }
}