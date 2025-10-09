using GamePlay.Character;
using GamePlay.Battle;
using UnityEngine;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "CardEffect_Block", menuName = "MyMenu/CardEffect/Block")]
  public class BlockEffectSO : GameEffectSO
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