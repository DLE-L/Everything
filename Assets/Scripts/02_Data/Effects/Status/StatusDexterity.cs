using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Status
{
  [CreateAssetMenu(fileName = "Status_Dexterity_", menuName = "MyMenu/Effect/Status/Dexterity")]
  public class StatusDexterity : StatusEffectSO
  {
    public override int GetAdditiveGainBlock(Unit owner)
    {
      owner.StatusEffects.TryGetValue(this, out ActiveStatusData datas);
      return datas.value;
    }
  }
}
