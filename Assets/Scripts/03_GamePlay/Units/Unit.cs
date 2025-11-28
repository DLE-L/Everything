using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Battle;
using Data.Effect;
using Core.Event;
using Data.Units;

namespace GamePlay.Units
{
  public abstract class Unit : MonoBehaviour
  {
    public event Action<Unit> OnDeath;
    public StatData Stat { get; protected set; }
    public readonly Dictionary<StatusEffectSO, ActiveStatusData> StatusEffects = new();
    protected TurnOwner Team { get; set; }
    public bool IsDie { get; private set; }

    protected virtual void HandleTurnStart(TurnOwner turnOwner)
    {
      if (turnOwner != Team) return;
      
      ResetBlock();
      ResetEnergy();
      ProcessTurnStartEffects();
    }

    public void ApplyStatusEffect(StatusEffectSO effect, int duration, int value)
    {
      if (!StatusEffects.TryGetValue(effect, out var data))
      {
        data = new ActiveStatusData() { duration = duration, value = value };
        StatusEffects.Add(effect, data);
        effect.OnApply(this, ref data);
        Debug.Log($"{effect.Name} 효과 신규 적용. 남은 턴: {data.duration}, 수치: {data.value}");
        return;
      }

      effect.OnReapply(ref data, duration, value);
      StatusEffects[effect] = data;
      Debug.Log($"{effect.Name} 효과 중첩/갱신. 남은 턴: {data.duration}, 수치: {data.value}");
    }

    private void ProcessTurnStartEffects()
    {
      foreach (var effect in StatusEffects.Keys)
      {
        var activeStatusData = StatusEffects[effect];
        effect.OnTurnStart(this, ref activeStatusData);
        StatusEffects[effect] = activeStatusData;
      }
      //Debug.Log($"{name}'s Turn Start Effects");
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
      //Debug.Log($"Addictive Damage : {finalDamage}");

      // 2. 곱연산
      float multiple = 1.0f;
      foreach (var effect in this.StatusEffects.Keys)
      {
        multiple *= effect.GetOutgoingMultiplicativeModifier(this);
      }
      finalDamage *= multiple;
      //Debug.Log($"Multiple Damage : {finalDamage}");

      // 3. 데미지 전달
      BattleEvent.RaiseDealDamage(this, target, Mathf.FloorToInt(finalDamage));
      target.TakeDamage(this, Mathf.FloorToInt(finalDamage));
    }

    public void TakeDamage(Unit attacker, int damage)
    {
      float calculateDamage = damage;

      // 1. 합연산
      int additive = 0;
      foreach (var effect in StatusEffects.Keys)
      {
        calculateDamage += effect.GetIncomingAdditiveBonus(this);
      }
      calculateDamage += additive;
      //Debug.Log($"Modified Additive Damage : {additive}");

      // 2. 곱연산      
      var multiple = 1.0f;
      foreach (var effect in this.StatusEffects.Keys)
      {
        multiple *= effect.GetIncomingMultiplicativeModifier(this);
      }
      calculateDamage *= multiple;
      //Debug.Log($"Modified Multiple Damage : {finalDamage}");
      
      var finalDamage = Mathf.FloorToInt(calculateDamage);
      
      // 3. 피격시 발동 효과
      foreach (var effect in StatusEffects.Keys.ToList())
      {
        ActiveStatusData data = StatusEffects[effect];
        effect.OnOwnerTakesDamage(this, ref data, finalDamage);
        StatusEffects[effect] = data;
      }
      
      // 4. 피격 실행 & 체력 피해량, 사망여부 체크
      var (loosHp, isDead) = Stat.ApplyDamage(Mathf.FloorToInt(finalDamage));
      
      BattleEvent.RaiseTakeDamage(attacker, this, loosHp);
      BattleEvent.RaiseDamageFeedback(this, loosHp);
      Debug.Log($"{attacker.name} attack {this.name}. Take Damage {loosHp}. Remain HP: {Stat.HP}");

      if (!isDead) return;
      
      Die(); 
      Debug.Log($"{name} is Dead");
    }

    public void Heal(int heal)
    {
      int calculateHeal = heal;

      foreach (var effect in StatusEffects.Keys)
      {

      }
      
      Stat.Heal(calculateHeal);
      BattleEvent.RaiseHeal(this, calculateHeal);
      Debug.Log($"{calculateHeal}체력 획득. 현재체력: {Stat.HP}");
    }

    public virtual void GainBlock(int block)
    {
      int finalBlock = block;

      foreach (var effect in StatusEffects.Keys)
      {
        finalBlock += effect.GetAdditiveGainBlock(this);
      }
      //Debug.Log($"Addictive Block : {finalBlock}");

      Stat.Block += finalBlock;
      BattleEvent.RaiseGainBlock(this, finalBlock);
      //Debug.Log($"{this.name} Block {Stat.Block}");
    }

    private void ResetBlock()
    {
      Stat.Block = 0;
      //Debug.Log($"Return {name} Reset Block");
    }

    private void ResetEnergy()
    {
      Stat.Energy = 3;
      //Debug.Log($"Return {name} Reset Energy");
    }

    private void Die()
    {
      OnDeath?.Invoke(this);
      IsDie = true;
      //Debug.Log($"{this.name} is die");
    }
    
    void OnEnable()
    {
      BattleEvent.OnTurnStart += HandleTurnStart;
      
      OnEnableOverride();
    }
    void OnDisable()
    {
      BattleEvent.OnTurnStart -= HandleTurnStart;
      OnDisableOverride();
    }

    protected virtual void OnEnableOverride(){}
    protected virtual void OnDisableOverride(){}

  }
}