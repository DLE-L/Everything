using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Block_", menuName = "MyMenu/Effect/Card/Block")]
  public class EffectBlock : GameEffectSO
  {
    [SerializeField] private int Amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user != null)
      {
        user.GainBlock(Amount);
        Debug.Log($"[Block Effect][{user.name} is Gain Block {Amount}]");
      }
      else
      {
        Debug.Log($"[Block Effect][User is null]");
      }
    }
  }  
}