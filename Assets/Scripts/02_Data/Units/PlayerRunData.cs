using System.Collections.Generic;
using Data.Collectible.Card;
using Data.Collectible.Relic;
using System;
using System.Linq;

namespace Data.Units
{
  [Serializable]
  public class PlayerRunData
  {
    public StatData Stat;
    public int RunStateGold;

    public HashSet<RelicSO> Relics = new();
    public Dictionary<string, int> Items = new();
    public List<RuntimeCard> Deck;

    public PlayerRunData(int maxHp, Dictionary<CardSO, int> deck, int takeGold = 0)
    {
      RunStateGold = takeGold;
      Deck = deck.SelectMany(pair => 
        Enumerable.Range(0, pair.Value)
                  .Select(_ => new RuntimeCard(pair.Key))
        ).ToList();
      Stat = new StatData
      {
        MaxHP = maxHp,
        HP = maxHp,
        Energy = 3,
        Block = 0
      };
    }
  }
}
