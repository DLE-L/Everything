using GamePlay.Character;
using GamePlay.Battle;
using UnityEngine;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "CardEffect_Exhaust", menuName = "MyMenu/CardEffect/Exhaust")]
  public class ExhaustEffectSO : GameEffectSO
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