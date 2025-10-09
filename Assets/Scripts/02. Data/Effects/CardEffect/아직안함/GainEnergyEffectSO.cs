using UnityEngine;
using GamePlay.Character;
using GamePlay.Battle;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "CardEffect_GainEnergy", menuName = "MyMenu/CardEffect/GainEnergy")]
  public class GainEnergyEffectSO : GameEffectSO
  {
    [SerializeField] private int Amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (user != null)
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