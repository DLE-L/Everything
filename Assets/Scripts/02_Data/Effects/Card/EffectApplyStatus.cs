using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_ApplyStatus_", menuName = "MyMenu/Effect/Card/ApplyStatus")]
  public class EffectApplyStatus : GameEffectSO
  {
    [Header("Status Effect Scriptable Object")]
    [SerializeField] private StatusEffectSO StatusEffectToApply;
    [Header("지속 시간")]
    [SerializeField] private int Duration;
    [Header("적용 수치")]
    [SerializeField] private int Value;
    public override void Execute(Unit user, Unit target)
    {
      if (target is not null && StatusEffectToApply is not null)
      {
        target.ApplyStatusEffect(StatusEffectToApply, Duration, Value);
        //Debug.Log($"[Apply Status Effect][{user.name}, {target.name} is Exist]");
      }
      else
      {
        Debug.Log($"[Apply Status Effect][({user.name} | {target.name} | StatusEffectToApply) is null]");
      }
    }
  }
}

