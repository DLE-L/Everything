using System;
using UnityEngine;
using Utils;

namespace Units
{
  public abstract class Unit : MonoBehaviour, IHealthSystem
  {
    public StatData Stat { get; set; }
    public event Action<Unit> OnDeath;

    public virtual void Damaged(int damage)
    {
      Stat.Hp -= damage;
      Debug.Log($"Damage: {damage}, {gameObject.name} HP: {Stat.Hp}");
      if (Stat.Hp <= 0)
      {
        Die();
      }
    }

    public virtual void Heal(int heal)
    {
      Stat.Hp += heal;
      Debug.Log($"Heal: {heal}, {gameObject.name} HP: {Stat.Hp}");
      if (Stat.Hp > Stat.MaxHp)
      {
        Stat.Hp = Stat.MaxHp;
      }
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