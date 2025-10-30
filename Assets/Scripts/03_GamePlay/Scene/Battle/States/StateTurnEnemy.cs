using System;
using Core;
using Core.Event;
using GamePlay.Units;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateTurnEnemy : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;

    public StateTurnEnemy(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }

    public async void Enter()
    {
      try
      {
        //Debug.Log($"-Enemy Turn-");
        BattleEvent.RaiseEnemyTurnStart();
        foreach (var unit in _manager.UnitManager.EnemyTeam)
        {
          if (unit is not EnemyController enemy) return;
          var isActSuccess = await enemy.EnemyActing();
          if (!isActSuccess) return;
        }

        _fsm.ChangeState(new StateTurnEnd(_manager, _fsm, TurnOwner.EnemyTeam));
      }
      catch (Exception e)
      {
        Debug.LogWarning($"StateTurnEnemy warning: {e.Message}");
      }
    }

    public void Execute() { }
    
    public void Exit()
    {
      BattleEvent.RaiseEnemyTurnEnd();
    }
  }
}