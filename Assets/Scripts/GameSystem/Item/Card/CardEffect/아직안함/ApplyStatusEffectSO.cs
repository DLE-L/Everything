using Units;
using UnityEngine;
using GameSystems.Scene.Battle;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_ApplyStatus", menuName = "MyMenu/CardEffect/ApplyStatus")]
  public class ApplyStatusEffectSO : ItemEffectSO
  {
    public StatusEffectSO StatusEffect;
    public override void Execute(Unit user, Unit target)
    {
      if (target != null)
      {
        
        Debug.Log($"[Apply Status Effect][{user.name} is Damage {target.name}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][타겟 {target.name}이 존재하지 않습니다.]");
      }
    }
  }  
}