
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StatePlayerStart : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StatePlayerStart(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      _stateSystem.ChangeState(new StatePlayerTurn(_battleManager, _stateSystem));
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }

    private void Shuffle(List<string> deck)
    {
      System.Random random = new();
      for (int i = 0; i < deck.Count - 1; i++)
      {
        var randomIndex = random.Next(i, deck.Count);
        (deck[i], deck[randomIndex]) = (deck[randomIndex], deck[i]);
      }
    }
  }
}