using UnityEngine;
using Core;
using System.Collections.Generic;
using Core.Event;
using Data.Collectible.Card;
using UIs.Battle;
using GamePlay.Units;

namespace GamePlay.Battle.State
{
  public class StateTurnPlayer : IBattleState
  {
    private readonly BattleManager _manager;
    private StateMachine _fsm;
    private Unit _playerUnit;

    public StateTurnPlayer(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }

    public void Enter()
    {
      //Debug.Log($"-Player Turn-");
      BattleEvent.RaisePlayerTurnStart();
    }

    public void Execute() { }

    public void Exit()
    {
      BattleEvent.RaisePlayerTurnEnd();
    }
  }
}