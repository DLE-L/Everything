using System;

namespace Utils
{
  [Serializable]
  public class StatData
  {
    public int MaxHp;
    public int Hp;
    public int MaxEnergy;
    public int Energy;
  }

  public interface IStatSystem
  {
    public int Hp { get; }
    public int MaxHp { get; }
    public int Energy { get; }
    public int MaxEnergy { get; }

    public void Damaged(int damage);

    public void Heal(int heal);

    public bool IsDie();
  }
}