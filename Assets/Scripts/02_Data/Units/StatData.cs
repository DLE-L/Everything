using System;
using UnityEngine;

namespace Data.Units
{
  [Serializable]
  public class StatData
  {
    public int HP;
    public int MaxHP;
    public int Energy;
    public int Block;

    public StatData() { }
    public StatData(StatData stat)
    {
      HP = stat.HP;
      MaxHP = stat.MaxHP;
      Energy = stat.Energy;
      Block = stat.Block;
    }

    public (int actualHPLoss, bool isDead) ApplyDamage(int finalDamage)
    {
      if (finalDamage <= 0) return (0, false);
      
      int damageToHp = finalDamage;
      if (Block > 0)
      {
        if (Block >= finalDamage)
        {
          Block -= finalDamage;
          damageToHp = 0;
        }
        else
        {
          damageToHp = finalDamage - Block;
          Block = 0;
        }
      }

      int actualHPLoss = 0;
      if (damageToHp > 0)
      {
        actualHPLoss = Mathf.Min(HP, damageToHp);
        HP -= damageToHp;
      }

      if (HP > 0) return (actualHPLoss, false);
      
      HP = 0;
      return (actualHPLoss, true);
    }
    
    public void Heal(int amount)
    {
      HP += amount;
      if (HP > MaxHP)
      {
        HP = MaxHP;
      }
    }
  }
}