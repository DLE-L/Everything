using System;
using Core;
using Core.Event;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateSetupBattle : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;
    private readonly TurnOwner _turnOwner;

    public StateSetupBattle(BattleManager manager, StateMachine fsm, TurnOwner owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }

    public async void Enter()
    {
      try
      {
        Debug.Log($"---Battle Setup---");
        BattleEvent.RaiseBattleStart();
        await _manager.UnitManager.Init();
        _fsm.ChangeState(new StateTurnStart(_manager, _fsm, _turnOwner));
      }
      catch (Exception e)
      {
        Debug.LogException(e);
      }
    }

    public void Execute() { }
    public void Exit() { }
  }
}