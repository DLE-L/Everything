using System;
using UnityEngine;
using Utils;
using Item;
using System.Collections.Generic;
using GameSystems.Scene.Battle;
using System.Linq;

namespace Units
{
  public abstract class Unit : MonoBehaviour
  {
    public virtual StatData Stat { get; set; }
    public event Action<Unit> OnDeath;
    public Dictionary<StatusEffectSO, ActiveStatusData> StatusEffects = new();
    public TurnOwner Team { get; private set; }

    public void Initialize(TurnOwner team)
    {
      Team = team;
    }

    public virtual void HandleTurnStart(TurnOwner turnOwner)
    {
      if (turnOwner == Team)
      {
        ResetBlock();
        ResetEnergy();
        ProcessTurnStartEffects();
      }
    }

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

    public void DealDamage(Unit target, int damage)
    {
      float finalDamage = damage;

      // 1. 합연산
      int additive = 0;
      foreach (var effect in this.StatusEffects.Keys)
      {
        additive += effect.GetOutgoingAdditiveBonus(this);
      }
      finalDamage += additive;
      Debug.Log($"[Addictive Damage : {finalDamage}]");

      // 2. 곱연산
      float multiple = 1.0f;
      foreach (var effect in this.StatusEffects.Keys)
      {
        multiple *= effect.GetOutgoingMultiplicativeModifier(this);
      }
      finalDamage *= multiple;
      Debug.Log($"[Multiple Damage : {finalDamage}]");

      // 3. 데미지 전달
      target.TakeDamage(this, Mathf.FloorToInt(finalDamage));
    }

    public void TakeDamage(Unit attacker, int damage)
    {
      float finalDamage = damage;

      // 1. 합연산
      int additive = 0;
      foreach (var effect in StatusEffects.Keys)
      {
        finalDamage += effect.GetIncomingAdditiveBonus(this);
      }
      finalDamage += additive;
      Debug.Log($"[Modified Additive Damage : {additive}]");

      // 2. 곱연산      
      float multiple = 1.0f;
      foreach (var effect in this.StatusEffects.Keys)
      {
        multiple *= effect.GetIncomingMultiplicativeModifier(this);
      }
      finalDamage *= multiple;
      Debug.Log($"[Modified Multiple Damage : {finalDamage}]");

      // 3. 방어도 적용
      int damageAfterBlock = Mathf.FloorToInt(finalDamage) - Stat.Block;
      if (damageAfterBlock < 0) damageAfterBlock = 0;

      // 4. 체력 감소
      Stat.HP -= damageAfterBlock;
      if (Stat.HP <= 0) { Die(); return; }

      // 5. 피격시 발동 효과
      foreach (var effect in StatusEffects.Keys.ToList())
      {
        ActiveStatusData data = StatusEffects[effect];
        effect.OnOwnerTakesDamage(this, ref data, damageAfterBlock);
        StatusEffects[effect] = data;
      }

      // 6. 피격 이벤트 발생
      BattleEvent.RaiseTakeDamage(attacker, this, damageAfterBlock);
      Debug.Log($"[{attacker} attack {this}. Take Damage {damageAfterBlock}][Remain HP: {Stat.HP}]");
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
      int finalBlock = block;

      foreach (var effect in StatusEffects.Keys)
      {
        finalBlock += effect.GetAdditiveGainBlock(this);
      }
      Debug.Log($"[Addiitive Block : {finalBlock}]");

      Stat.Block += finalBlock;
      BattleEvent.RaiseGainBlock(this, finalBlock);
      Debug.Log($"{this.name} Block {Stat.Block}");
    }

    public void ResetBlock()
    {
      Stat.Block = 0;
      Debug.Log($"[Return {name} Reset Block]");
    }

    public void ResetEnergy()
    {
      Stat.Energy = Stat.MaxEnergy;
      Debug.Log($"[Return {name} Reset Energy]");
    }

    public virtual void Die()
    {
      OnDeath?.Invoke(this);
    }

    void OnEnable()
    {
      BattleEvent.OnTurnStart += HandleTurnStart;
    }
    void OnDisable()
    {
      BattleEvent.OnTurnStart -= HandleTurnStart;    
      
    }

  }
}