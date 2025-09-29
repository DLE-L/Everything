using UnityEngine;

namespace Item
{
  [CreateAssetMenu(fileName = "NewStatus", menuName = "MyMenu/StatusEffect/Poison")]
  public class StatusPoison : StatusEffectSO
  {
    public override void OnReapply(ref ActiveStatusData data, int newDuration, int newValue)
    {
      data.value += newValue;
    }
  }
}
