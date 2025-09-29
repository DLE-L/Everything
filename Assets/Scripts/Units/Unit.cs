using System;
using UnityEngine;
using Utils;
using Item;
using System.Collections.Generic;

namespace Units
{
  public abstract class Unit : MonoBehaviour
  {
    public virtual StatData Stat { get; set; }    
    public event Action<Unit> OnDeath;
    public Dictionary<StatusEffectSO, int> StatusEffect = new();
    
    public void Damaged(int damage)
    {
      int finalDamage = damage;

      foreach (var effect in StatusEffect.Keys)
      {
        finalDamage = effect.OnCalculateValue(this, finalDamage);
      }
      Stat.HP -= finalDamage;
      if (Stat.HP <= 0)
      {
        Die();
        return;
      }
      Debug.Log($"[{damage}피격][현재체력: {Stat.HP}]");
    }

    public void Heal(int heal)
    {
      int finalHeal = heal;

      foreach (var effect in StatusEffect.Keys)
      {
        finalHeal = effect.OnCalculateValue(this, finalHeal);
      }
      Stat.HP += heal;
      if (Stat.HP > Stat.MaxHP)
      {
        Stat.HP = Stat.MaxHP;
      }
      Debug.Log($"[{heal}체력 획득][현재체력: {Stat.HP}]");
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