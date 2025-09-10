
using System.Collections.Generic;
using GameSystems;
using Utils;

namespace Card
{
  public static class CardDatabase
  {
    public static Dictionary<string, CardSO> cardDatabase = new(); // Dictionary<CardId, CardData>      
    private static HashSet<string> _defaultCardIDs = new()
    {
      "Attack_Strike",
      "Deffence_Defend",
      "Attack_Bash",
      "Skill_Survivor"
    };

    public static void Init()
    {
      LoadCardData();
    }

    public async static void LoadCardData()
    {
      var cardList = await AssetLoader.LoadAssetLabelAsync<CardSO>("Card");
      foreach (var card in cardList)
      {        
        cardDatabase.Add(card.CardId, card);
      }
    }

    public static CardSO GetCardData(string cardId)
    {
      if (cardDatabase.ContainsKey(cardId))
      {
        return cardDatabase[cardId];
      }
      return null;
    }

    public static bool IsDefaultCard(string cardId)
    {
      return _defaultCardIDs.Contains(cardId);
    }
  }
}