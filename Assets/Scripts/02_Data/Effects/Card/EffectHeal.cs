using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Heal_", menuName = "MyMenu/Effect/Card/Heal")]
  public class EffectHeal : GameEffectSO
  {
    [SerializeField] private int amount;
    public override void Execute(Unit user, Unit target)
    {
      if (user is not null)
      {
        user.Heal(amount);
        //Debug.Log($"[Heal Effect][{user.name} is Healing {amount}]");
      }
      else
      {
        Debug.Log($"[Heal Effect][User is null]");
      }
    }
  }
}