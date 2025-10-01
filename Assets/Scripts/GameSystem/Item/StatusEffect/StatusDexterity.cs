using UnityEngine;
using Units;

namespace Item
{
  [CreateAssetMenu(fileName = "Status_Dexterity_", menuName = "MyMenu/StatusEffect/Dexterity")]
  public class StatusDexterity : StatusEffectSO
  {
    public override int GetAdditiveGainBlock(Unit owner)
    {
      owner.StatusEffects.TryGetValue(this, out ActiveStatusData datas);
      return datas.value;
    }
  }
}