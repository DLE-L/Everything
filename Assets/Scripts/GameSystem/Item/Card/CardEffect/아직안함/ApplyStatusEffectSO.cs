using Units;
using UnityEngine;
using GameSystems.Scene.Battle;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_ApplyStatus", menuName = "MyMenu/CardEffect/ApplyStatus")]
  public class ApplyStatusEffectSO : ItemEffectSO
  {
    public StatusEffectSO StatusEffect;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {      
      if (target != null)
      { 
        StatusEffect.OnApply(user, target);
        Debug.Log($"[Apply Status Effect][{user.name}, {target.name} is Exist]");
      }
      else
      {
        Debug.Log($"[Damage Effect][{user.name}, {target.name} is null]");
      }
    }
  }  
}