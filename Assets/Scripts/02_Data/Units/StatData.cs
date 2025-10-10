using System;

namespace Data.Units
{
  [Serializable]
  public class StatData
  {
    public int HP;
    public int MaxHP;
    public int Energy;
    public int MaxEnergy;
    public int Block;

    public StatData() { }
    public StatData(StatData stat)
    {
      HP = stat.HP;
      MaxHP = stat.MaxHP;
      Energy = stat.Energy;
      MaxEnergy = stat.MaxEnergy;
      Block = stat.Block;
    }
  }
}