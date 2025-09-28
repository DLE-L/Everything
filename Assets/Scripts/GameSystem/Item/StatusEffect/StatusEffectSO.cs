using Units;
using UnityEngine;

namespace Item
{
  public abstract class StatusEffectSO : ScriptableObject
  {
    public string Name;
    public Sprite Icon;
    public string Description;
    public StatusType Type;
    public int Duration;
    public bool IsStackable;
    public abstract void OnApply(Unit user, Unit target);
    public virtual void ProcessTurnStartEffects(Unit self) { }
    public virtual void OnRemove(Unit user, Unit target) { }
    public virtual int OnCalculateValue(Unit target, int originalValue) { return originalValue; }
  }

  public enum StatusType
  {
    Buff,   // 플레이어/적에게 이로운 효과 (예: 힘, 재생)
    Debuff  // 플레이어/적에게 해로운 효과 (예: 약화, 취약)
  }
}