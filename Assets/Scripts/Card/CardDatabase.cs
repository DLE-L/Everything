
using System.Collections.Generic;
using GameSystem;
using Utils;

namespace Card
{
  public static class CardDatabase
  {
    public static Dictionary<string, CardData> cardDatabase = new(); // Dictionary<CardId, CardData>    

    public static void Init()
    {
      LoadCardData();
    }

    public async static void LoadCardData()
    {
      var cardList = await AssetLoader.LoadAssetLabelAsync<CardSO>("Card");
      foreach (var card in cardList)
      {
        CardData data = new(card);
        cardDatabase.Add(card.CardId, data);
      }
    }

    public static CardData GetCardData(string cardId)
    {
      if (cardDatabase.ContainsKey(cardId))
      {
        return cardDatabase[cardId];
      }
      return null;
    }
  }
}