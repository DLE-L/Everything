using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_GainEnergy_", menuName = "MyMenu/Effect/Card/GainEnergy")]
  public class EffectGainEnergy : GameEffectSO
  {
    [SerializeField] private int Amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user is not null)
      {
        user.GainBlock(Amount);        
        Debug.Log($"[GainEnergy Effect][{user.name} is Gain Block {Amount}]");
      }
      else
      {
        Debug.Log($"[GainEnergy Effect][User is null]");
      }
    }
  }
}