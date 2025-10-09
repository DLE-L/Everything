using System.Collections.Generic;
using Data.Card;
using Data.Relic;
using System;
using Utils;

namespace Data.Character
{
  [Serializable]
  public class PlayerRunData
  {
    public StatData Stat;
    public int RunStateGold;

    public HashSet<RelicSO> Relics = new();
    public Dictionary<string, int> Items = new();
    public Dictionary<CardSO, int> Deck = new();

    public PlayerRunData(int maxHp, Dictionary<CardSO, int> deck, int takeGold = 0)
    {
      RunStateGold = 0;
      Deck = new(deck);
      Stat.MaxHP = maxHp;
      Stat.HP = maxHp;
      Stat.MaxEnergy = 3;
      Stat.Energy = 3;
      Stat.Block = 0;
    }
  }
}
