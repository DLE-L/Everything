using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Heal_", menuName = "MyMenu/Effect/Card/Heal")]
  public class EffectHeal : GameEffectSO
  {
    [SerializeField] private int Amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user is not null)
      {
        user.Heal(Amount);
        Debug.Log($"[Heal Effect][{user.name} is Healing {Amount}]");
      }
      else
      {
        Debug.Log($"[Heal Effect][User is null]");
      }
    }
  }
}