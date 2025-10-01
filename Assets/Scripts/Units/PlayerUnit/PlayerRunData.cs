using System.Collections.Generic;
using Item;
using System;
using Utils;

namespace Units
{
  [Serializable]
  public class PlayerRunData
  {
    public StatData Stat;
    public int RunStateGold;

    public HashSet<string> Relics = new();
    public Dictionary<string, int> Items = new();
    public Dictionary<string, int> Deck = new();

    public PlayerRunData(int maxHp, Dictionary<string, int> deck, int takeGold = 0)
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
