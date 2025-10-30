using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_GainEnergy_", menuName = "MyMenu/Effect/Card/GainEnergy")]
  public class EffectGainEnergy : GameEffectSO
  {
    [SerializeField] private int amount;
    public override void Execute(Unit user, Unit target)
    {
      if (user is not null)
      {
        user.GainBlock(amount);        
        //Debug.Log($"[GainEnergy Effect][{user.name} is Gain Block {amount}]");
      }
      else
      {
        Debug.Log($"[GainEnergy Effect][User is null]");
      }
    }
  }
}