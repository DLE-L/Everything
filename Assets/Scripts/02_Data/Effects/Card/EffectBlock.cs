using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Block_", menuName = "MyMenu/Effect/Card/Block")]
  public class EffectBlock : GameEffectSO
  {
    [SerializeField] private int amount;
    public override void Execute(Unit user, Unit target)
    {
      if (user is not null)
      {
        user.GainBlock(amount);
        //Debug.Log($"[Block Effect][{user.name} is Gain Block {amount}]");
      }
      else
      {
        Debug.Log($"[Block Effect][User is null]");
      }
    }
  }  
}