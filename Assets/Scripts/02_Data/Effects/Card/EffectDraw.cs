using Core.Event;
using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Draw_", menuName = "MyMenu/Effect/Card/Draw")]
  public class EffectDraw : GameEffectSO
  {
    [SerializeField] private int amount;
    public override void Execute(Unit user, Unit target)
    {
      if (user is not null)
      {
        BattleEvent.RaiseRequestDraw(amount);
        //Debug.Log($"[Draw Effect][{user.name} is Draw Card]");
      }
      else
      {
        Debug.Log($"[Draw Effect][User is null]");
      }
    }
  }  
}