using System;
using UnityEngine;
using Utils;
using Item;
using System.Collections.Generic;
using GameSystems.Scene.Battle;

namespace Units
{
  public abstract class Unit : MonoBehaviour
  {
    public virtual StatData Stat { get; set; }
    public event Action<Unit> OnDeath;
    public Dictionary<StatusEffectSO, ActiveStatusData> StatusEffects = new();

    public void ApplyStatusEffect(StatusEffectSO effect, int duration, int value)
    {
      if (StatusEffects.TryGetValue(effect, out ActiveStatusData data) == false)
      {
        data = new() { duration = duration, value = value };
        StatusEffects.Add(effect, data);
        effect.OnApply(this, ref data);
        Debug.Log($"[{effect.Name}] 효과 신규 적용. 남은 턴: {data.duration}, 수치: {data.value}");
        return;
      }

      effect.OnReapply(ref data, duration, value);
      StatusEffects[effect] = data;
      Debug.Log($"[{effect.Name}] 효과 중첩/갱신. 남은 턴: {data.duration}, 수치: {data.value}");
    }

    public void ProcessTurnStartEffects()
    {
      foreach (StatusEffectSO effect in StatusEffects.Keys)
      {
        ActiveStatusData data = StatusEffects[effect];
        effect.OnTurnStart(this, ref data);
        StatusEffects[effect] = data;
      }
      Debug.Log($"[{name}'s Turn Start Effects]");
    }

    public void Damaged(int damage)
    {
      int finalDamage = damage;

      foreach (var effect in StatusEffects.Keys)
      {
        finalDamage = effect.OnBeforeTakeDamage(finalDamage);
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

      foreach (var effect in StatusEffects.Keys)
      {

      }
      Stat.HP += finalHeal;
      if (Stat.HP > Stat.MaxHP)
      {
        Stat.HP = Stat.MaxHP;
      }
      Debug.Log($"[{finalHeal}체력 획득][현재체력: {Stat.HP}]");
    }

    public virtual void GainBlock(int block)
    {
      Stat.Block += block;
      Debug.Log($"Block: {block}, {gameObject.name} Block: {Stat.Block}");
    }

    public void ResetBlock()
    {
      Stat.Block = 0;
      Debug.Log($"[Return {name} Reset Block]");
    }

    public virtual void Die()
    {
      OnDeath?.Invoke(this);
    }

    void OnEnable()
    {

    }

  }
}