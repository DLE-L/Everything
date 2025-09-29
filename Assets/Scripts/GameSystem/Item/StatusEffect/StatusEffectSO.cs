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
    public bool IsStackable;
    public virtual void OnRemove(Unit owner) { }
    public virtual void OnApply(Unit target, ref ActiveStatusData data) { }    
    public virtual void OnReapply(ref ActiveStatusData data, int newDuration, int newValue) { }    
    public virtual void OnTurnStart(Unit owner, ref ActiveStatusData data) { }    
    public virtual int OnBeforeDealDamage(int originalDamage) => originalDamage;
    public virtual int OnBeforeTakeDamage(int originalDamage) => originalDamage;
  }

  [SerializeField]
  public struct ActiveStatusData
  {
    public int duration;
    public int value;
  }

  public enum StatusType
  {
    Buff,   // 플레이어/적에게 이로운 효과 (예: 힘, 재생)
    Debuff  // 플레이어/적에게 해로운 효과 (예: 약화, 취약)
  }
}