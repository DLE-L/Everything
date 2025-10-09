using UnityEngine;
using GamePlay.Character;

namespace Data.Effect.Status
{
  [CreateAssetMenu(fileName = "Status_Strength_", menuName = "MyMenu/StatusEffect/Strength")]
  public class StatusStrength : StatusEffectSO
  {
    public override int GetOutgoingAdditiveBonus(Unit owner)
    {
      owner.StatusEffects.TryGetValue(this, out ActiveStatusData datas);
      return datas.value;
    }
    public override void OnTurnStart(Unit owner, ref ActiveStatusData data)
    {
      data.value += 1;
    }
  }
}