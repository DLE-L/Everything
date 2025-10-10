using System.Collections.Generic;
using Data.Card;
using System;

namespace Data.Units
{  
  [Serializable]
  public class PlayerAccountData
  {
    // 유저 정보
    // public string PlayerID = ""; // TODO: 추후 DB구현시 필요
    // public string NickName = ""; // TODO: 추후 DB구현시 필요

    // 성장 요소    
    public int Gold;
    public HashSet<string> UnlockedCardIDs = new(); // 해금된 카드 ID 목록
    public HashSet<string> UnlockedRelicIDs = new(); // Dictionary<해금 요소 ID>
    public Dictionary<string, Dictionary<string, int>> Decks = new(); // Dictionary<덱ID, Dictionary<카드ID, 개수>>

    public bool IsCardUnlocked(string cardId)
    {
      return UnlockedCardIDs.Contains(cardId);
    }

    public bool AddCardToDeck(string deckId, string cardId)
    {
      if (Decks.ContainsKey(deckId) == false
          || IsCardUnlocked(cardId) == false) return false;


      Dictionary<string, int> deck = Decks[deckId];
      deck.TryGetValue(cardId, out int count);

      if (CardDatabase.IsDefaultCard(cardId) == false && count == 2) return false;

      deck[cardId] = count + 1;
      return true;
    }
  }
}