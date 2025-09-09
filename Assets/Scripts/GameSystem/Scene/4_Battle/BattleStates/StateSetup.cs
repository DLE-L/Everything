
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
      _battleManager.Shuffle(deck);
      _battleManager.DrawPile = deck;

      // 3. Setup 상태 종료(플레이어 턴 상태로 변경)
      _battleManager.ChangePlayerStartState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
      
    }
  }
}