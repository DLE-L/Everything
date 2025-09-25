using Units;
using UnityEngine;
using GameSystems.Scene.Battle;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_ModifyGoldGain", menuName = "MyMenu/CardEffect/ModifyGoldGain")]
  public class ModifyGoldGainEffectSO : ItemEffectSO
  {
    public int amount;
    public override void Execute(Unit user, Unit target)
    {
      if (target != null)
      {
        
        Debug.Log($"[Damage Effect][{user.name} is Damage {target.name}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][타겟 {target.name}이 존재하지 않습니다.]");
      }
    }
  }  
}