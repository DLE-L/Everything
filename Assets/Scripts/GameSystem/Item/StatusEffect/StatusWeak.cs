using UnityEngine;

namespace Item
{
  [CreateAssetMenu(fileName = "NewStatus", menuName = "MyMenu/StatusEffect/Weak")]
  public class StatusWeak : StatusEffectSO
  {
    public override int OnBeforeDealDamage(int originalDamage)
    {
      return Mathf.FloorToInt(originalDamage * 0.75f);
    }
    public override void OnReapply(ref ActiveStatusData data, int newDuration, int newValue)
    {
      data.duration = Mathf.Max(data.duration, newDuration);
    }  
  }
}
// 약화 : 최종 피해량이 25% 감소