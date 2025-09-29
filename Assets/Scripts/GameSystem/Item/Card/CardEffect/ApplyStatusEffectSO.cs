using Units;
using UnityEngine;
using GameSystems.Scene.Battle;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_ApplyStatus", menuName = "MyMenu/CardEffect/ApplyStatus")]
  public class ApplyStatusEffectSO : ItemEffectSO
  {
    [Header("Status Effect Scriptable Object")]
    public StatusEffectSO StatusEffectToApply;
    [Header("지속 시간")]
    public int Duration;
    [Header("적용 수치")]
    public int Value;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (target != null && StatusEffectToApply != null)
      {
        target.ApplyStatusEffect(StatusEffectToApply, Duration, Value);
        Debug.Log($"[Apply Status Effect][{user.name}, {target.name} is Exist]");
      }
      else
      {
        Debug.Log($"[Apply Status Effect][({user.name} | {target.name} | StatusEffectToApply) is null]");
      }
    }
  }
}