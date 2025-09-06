
using System.Collections.Generic;
using Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StateSetup : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StateSetup(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      PlayerAccountData account = _battleManager.Inventory.PlayerData;
      if (account == null) return;
      Dictionary<string, int> data = account.GetCurrentCardDeck();
      List<string> deck = new();
      foreach (var cardInfo in data)
      {
        for (int i = 0; i < cardInfo.Value; i++)
        {
          deck.Add(cardInfo.Key);
        }
      }
      // 2. 플레이어 덱 섞기
      Shuffle(deck);
      _battleManager.DrawPile = deck;

      _stateSystem.ChangeState(new StatePlayerStart(_battleManager, _stateSystem));
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