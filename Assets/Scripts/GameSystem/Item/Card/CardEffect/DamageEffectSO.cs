using Units;
using UnityEngine;
using GameSystems.Scene.Battle;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_Damage", menuName = "MyMenu/CardEffect/Damage")]
  public class DamageEffectSO : ItemEffectSO
  {
    [SerializeField] private int Amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user != null && target != null)
      {
        user.DealDamage(target, Amount);        
        Debug.Log($"[Damage Effect][{target.name} is Damaged {user.name}, {Amount}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][User & Target is null]");
      }
    }
  }
}