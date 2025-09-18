using System.Collections.Generic;
using GameSystems.Scene.Game;
using Card;

namespace Utils
{
  public static class EventDatabase
  {
    public static Dictionary<string, EventSO> events = new();
    public static EventSO CurrentEvent;

    public async static void LoadEventData()
    { 
      var eventList = await AssetLoader.LoadAssetLabelAsync<EventSO>("Event");
      foreach (var item in eventList)
      {
        events.Add(item.Name, item);
      }
    }

  }
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

  public static class EncounterDatabase
  {
    public static Dictionary<string, EncounterSO> encounters = new();
    public static EncounterSO CurrentEncounter;

    public async static void LoadEncounterData()
    {
      var encounterList = await AssetLoader.LoadAssetLabelAsync<EncounterSO>("Encounter");
      foreach (var encounter in encounterList)
      {
        encounters.Add(encounter.name, encounter);        
      }
    } 
  }

}