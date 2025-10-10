using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;
namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Discard_", menuName = "MyMenu/Effect/Card/Discard")]
  public class EffectDiscard : GameEffectSO
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