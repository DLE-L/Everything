using UnityEngine;
using GamePlay.Character;
using GamePlay.Battle;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "CardEffect_GainGold", menuName = "MyMenu/CardEffect/GainGold")]
  public class GainGoldEffectSO : GameEffectSO
  {
    public int amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (target != null)
      {
        
        Debug.Log($"[Damage Effect][{user.name} is Damage {target.name}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][타겟 {target.name}이 존재하지 않습니다.]");
      }
    }
  }  
}