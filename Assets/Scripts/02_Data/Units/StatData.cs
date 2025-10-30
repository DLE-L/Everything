using System;

namespace Data.Units
{
  [Serializable]
  public class StatData
  {
    public int HP;
    public int MaxHP;
    public int Energy;
    public int Block = 0;

    public StatData() { }
    public StatData(StatData stat)
    {
      HP = stat.HP;
      MaxHP = stat.MaxHP;
      Energy = stat.Energy;
      Block = stat.Block;
    }
  }
}