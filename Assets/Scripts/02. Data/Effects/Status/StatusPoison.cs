using UnityEngine;

namespace Data.Effect.Status
{
  [CreateAssetMenu(fileName = "Status_Poison_", menuName = "MyMenu/StatusEffect/Poison")]
  public class StatusPoison : StatusEffectSO
  {
    
    public override void OnReapply(ref ActiveStatusData data, int newDuration, int newValue)
    {
      data.value += newValue;
    }
  }
}
