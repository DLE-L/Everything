using Units;
using UnityEngine;
using GameSystems.Scene.Battle;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_Heal", menuName = "MyMenu/CardEffect/Heal")]
  public class HealEffectSO : ItemEffectSO
  {
    [SerializeField] private int Amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user != null)
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