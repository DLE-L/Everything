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
    public virtual void OnRemove(Unit owner) { }
    public virtual void OnApply(Unit target, ref ActiveStatusData data) { }
    public virtual void OnReapply(ref ActiveStatusData data, int newDuration, int newValue) { }
    public virtual void OnTurnStart(Unit owner, ref ActiveStatusData data) { }
    public virtual void OnOwnerTakesDamage(Unit owner, ref ActiveStatusData data, int damageAmount) { }
    public virtual int GetOutgoingAdditiveBonus(Unit owner) => 0;
    public virtual float GetOutgoingMultiplicativeModifier(Unit owner) => 1f;
    public virtual int GetIncomingAdditiveBonus(Unit owner) => 0;
    public virtual float GetIncomingMultiplicativeModifier(Unit owner) => 1f;
    public virtual int GetAdditiveGainBlock(Unit owner) => 0;
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