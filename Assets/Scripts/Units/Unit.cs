using System;
using UnityEngine;
using Utils;
using Item;

namespace Units
{
  public abstract class Unit : MonoBehaviour, IHealthSystem
  {
    public virtual StatData Stat { get; set; }
    public event Action<Unit> OnDeath;

    public void Damaged(int amount)
    {
      Stat.HP -= amount;
      if (Stat.HP <= 0)
      {
        Die();
        return;
      }
      Debug.Log($"[{amount}피격][현재체력: {Stat.HP}]");
    }

    public void Heal(int amount)
    {
      Stat.HP += amount;
      if (Stat.HP > Stat.MaxHP)
      {
        Stat.HP = Stat.MaxHP;
      }
      Debug.Log($"[{amount}체력 획득][현재체력: {Stat.HP}]");
    }
    
    public virtual void GainBlock(int block)
    {
      Stat.Block += block;
      Debug.Log($"Block: {block}, {gameObject.name} Block: {Stat.Block}");
    }

    public virtual void Die()
    {
      OnDeath?.Invoke(this);
    }
  }
}