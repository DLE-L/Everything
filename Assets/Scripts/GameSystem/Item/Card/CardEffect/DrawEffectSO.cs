using Units;
using UnityEngine;
using GameSystems.Scene.Battle;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_Draw", menuName = "MyMenu/CardEffect/Draw")]
  public class DrawEffectSO : ItemEffectSO
  {
    public int amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user != null)
      {
        manager.CardManager.Draw(amount);
        Debug.Log($"[Draw Effect][{user.name} is Draw Card]");
      }
      else
      {
        Debug.Log($"[Draw Effect][User is null]");
      }
    }
  }  
}