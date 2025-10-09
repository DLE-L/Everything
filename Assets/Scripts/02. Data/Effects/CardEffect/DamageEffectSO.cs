using UnityEngine;
using GamePlay.Character;
using GamePlay.Battle;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "CardEffect_Damage", menuName = "MyMenu/CardEffect/Damage")]
  public class DamageEffectSO : GameEffectSO
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