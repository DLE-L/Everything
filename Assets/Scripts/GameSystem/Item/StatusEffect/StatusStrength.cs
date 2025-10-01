using Units;
using UnityEngine;
namespace Item
{
  [CreateAssetMenu(fileName = "Status_Strength_", menuName = "MyMenu/StatusEffect/Strength")]
  public class StatusStrength : StatusEffectSO
  {
    public override int GetOutgoingAdditiveBonus(Unit owner)
    {
      owner.StatusEffects.TryGetValue(this, out ActiveStatusData datas);
      return datas.value;
    }      
  }
}