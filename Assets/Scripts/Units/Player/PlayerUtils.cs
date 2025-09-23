using System.Collections.Generic;
using Item;
using System;
using Utils;

namespace Units.Player
{
  [Serializable]
  public class PlayerRunData
  {    
    public int HP;
    public int MaxHP;
    public int Energy;
    public int MaxEnergy;
    public int Block;
    public int RunStateGold;

    public HashSet<string> Relics = new();
    public Dictionary<string, int> Items = new();
    public Dictionary<string, int> Deck = new();

    public PlayerRunData(int maxHp)
    {
      RunStateGold = 0;
      HP = maxHp;
      MaxHP = maxHp;
      Energy = 3;
      MaxEnergy = 3;
      Block = 0;
    }

  }
  public class PlayerAccountData
  {
    // 유저 정보
    // public string PlayerID = ""; // TODO: 추후 DB구현시 필요
    // public string NickName = ""; // TODO: 추후 DB구현시 필요

    // 성장 요소    
    public int Gold;
    public HashSet<string> UnlockedCardIDs = new(); // 해금된 카드 ID 목록
    public HashSet<string> Unlocks = new(); // Dictionary<해금 요소 ID>
    public Dictionary<string, Dictionary<string, int>> Decks = new(); // Dictionary<덱ID, Dictionary<카드ID, 개수>>
    public string CurrentDeckID;

    public Dictionary<string, int> GetCurrentCardDeck()
    {
      if (string.IsNullOrEmpty(CurrentDeckID) || !Decks.TryGetValue(CurrentDeckID, out var deck))
      {
        UnityEngine.Debug.LogWarning($"현재 덱(ID: '{CurrentDeckID}')을 찾을 수 없습니다. 비어있는 덱을 반환합니다.");
        return new Dictionary<string, int>();
      }
      return deck;
    }

    public bool IsCardUnlocked(string cardId)
    {
      return UnlockedCardIDs.Contains(cardId);
    }

    public void DefaultCardDeck() // TODO: ScriptableObject로 빼서 더 관리하게 쉽게
    {/*
      기본 덱 구성:
      Strike (타격) x 4
      Defend (수비) x 4
      Bash (강타) x 1
      Survivor (생존 본능) x 1  */
      Dictionary<string, int> defaultDeck = new()
        {
          { "Attack_Strike", 4 },
          { "Deffence_Defend", 4 },
          { "Attack_Bash", 1 },
          { "Skill_Survivor", 1 }
        };

      foreach (var cardID in defaultDeck.Keys)
      {
        UnlockedCardIDs.Add(cardID);
      }

      string deckId = "Default";
      CurrentDeckID = deckId;
      if (!Decks.ContainsKey(deckId))
      {
        Decks[deckId] = new Dictionary<string, int>();
      }
      else
      {
        Decks[deckId].Clear();
      }

      foreach (var cardInfo in defaultDeck)
      {
        string cardId = cardInfo.Key;
        int count = cardInfo.Value;

        for (int i = 0; i < count; i++)
        {
          AddCardToDeck(deckId, cardId);
        }
      }
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
