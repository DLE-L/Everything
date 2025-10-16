using System.Collections.Generic;
using Data.Collectible.Card;
using Data.Collectible.Relic;
using System;
using Utils;

namespace Data.Units
{
  [Serializable]
  public class PlayerRunData
  {
    public StatData Stat;
    public int RunStateGold;

    public HashSet<RelicSO> Relics = new();
    public Dictionary<string, int> Items = new();
    public Dictionary<CardSO, int> Deck;

    public PlayerRunData(int maxHp, Dictionary<CardSO, int> deck, int takeGold = 0)
    {
      RunStateGold = takeGold;
      Deck = new(deck);
      Stat = new()
      {
        MaxHP = maxHp,
        HP = maxHp,
        MaxEnergy = 3,
        Energy = 3,
        Block = 0
      };
    }
  }
}
