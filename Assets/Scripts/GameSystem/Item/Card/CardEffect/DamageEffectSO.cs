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
      if (target != null)
      {
        target.Damaged(Amount);
        Debug.Log($"[Damage Effect][{target.name} is Damaged {user.name}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][Target is null]");
      }
    }
  }
}