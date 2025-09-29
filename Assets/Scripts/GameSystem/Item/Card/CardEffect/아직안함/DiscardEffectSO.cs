using Units;
using GameSystems.Scene.Battle;
using UnityEngine;
using System.Net.Mail;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_Discard", menuName = "MyMenu/CardEffect/Discard")]
  public class DiscardEffectSO : ItemEffectSO
  {
    public int amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user != null && user.CompareTag("Player"))
      {
        manager.CardManager.DiscardRandom(amount);
        Debug.Log($"[Discard Effect][{user.name} is Discard {amount}]");
      }
      else
      {
        Debug.Log($"[Discard Effect][{user.name} is null]");
      }
    }
  }
}