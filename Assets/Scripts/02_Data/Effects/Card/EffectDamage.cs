using Core.Event;
using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Damage_", menuName = "MyMenu/Effect/Card/Damage")]
  public class EffectDamage : GameEffectSO
  {
    [SerializeField] private int Amount;

    public override void Execute(Unit user, Unit target)
    {
      if (user is not null && target is not null)
      {
        user.DealDamage(target, Amount);
        //Debug.Log($"[Damage Effect][{target.name} is Damaged {user.name}, {Amount}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][User & Target is null]");
      }
    }
  }
}