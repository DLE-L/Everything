using GameSystems.Scene.Battle;
using Units;
using UnityEngine;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_Block", menuName = "MyMenu/CardEffect/Block")]
  public class BlockEffectSO : ItemEffectSO
  {
    [SerializeField] private int Amount;
    public override void Execute(Unit user, Unit target)
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