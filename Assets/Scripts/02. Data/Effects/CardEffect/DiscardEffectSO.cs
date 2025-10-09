using UnityEngine;
using GamePlay.Character;
using GamePlay.Battle;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "CardEffect_Discard", menuName = "MyMenu/CardEffect/Discard")]
  public class DiscardEffectSO : GameEffectSO
  {
    public int amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user != null)
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