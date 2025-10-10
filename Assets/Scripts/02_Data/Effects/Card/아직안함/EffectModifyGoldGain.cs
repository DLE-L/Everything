using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_ModifyGoldGain_", menuName = "MyMenu/Effect/Card/ModifyGoldGain")]
  public class EffectModifyGoldGain : GameEffectSO
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