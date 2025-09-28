using System;
using Units;
using UnityEngine;

namespace Item
{
  [CreateAssetMenu(fileName = "NewStatus", menuName = "MyMenu/StatusEffect/Weak")]
  public class StatusWeak : StatusEffectSO
  {
    public override void OnApply(Unit user, Unit target)
    {
      target.StatusEffect.Add(this, this.Duration);
      Debug.Log($"[Weak Debuff][{target.name} get Weak Debuff]");
    }
    
    public override int OnCalculateValue(Unit target, int originalValue)
    {
      return Mathf.FloorToInt(originalValue * 0.75f);
    }

    public override void OnRemove(Unit user, Unit target)
    {
      target.StatusEffect[this]--;
      if (target.StatusEffect[this] <= 0)
      {
        target.StatusEffect.Remove(this);
        Debug.Log($"[Weak Debuff][{target.name} remove Weak Debuff]");  
        return;
      }
      Debug.Log($"[Weak Debuff][{target.name} remain {Duration} turn]");
    }
  }
}
// 약화 : 최종 피해량이 25% 감소