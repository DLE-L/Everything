using System;
using Units.Player;

namespace Utils
{
  [Serializable]
  public class StatData
  {
    public int Hp;
    public int MaxHp;
    public int Energy;
    public int MaxEnergy;
    public int Block;

    public StatData() { }
    public StatData(StatData stat)
    {
      Hp = stat.Hp;
      MaxHp = stat.MaxHp;
      Energy = stat.Energy;
      MaxEnergy = stat.MaxEnergy;
      Block = stat.Block;
    }
  }

  public interface IHealthSystem
  {
    public void Damaged(int damage);
    public void Heal(int heal);
    public void GainBlock(int block);
    public void Die();
  }

  public interface IBattleState
  {
    public void Enter();
    public void Execute();
    public void Exit();
  }  
  
}