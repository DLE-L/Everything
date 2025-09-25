using System;
using Units.Player;

namespace Utils
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

  public interface IHealthSystem
  {
    public void Damaged(int amount);
    public void Heal(int amount);     
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