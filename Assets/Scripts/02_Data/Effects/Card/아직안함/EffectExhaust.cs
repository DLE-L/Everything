using GamePlay.Battle;
using UnityEngine;
using GamePlay.Units;

namespace Data.Effect.Card
{
  [CreateAssetMenu(fileName = "Effect_Exhaust_", menuName = "MyMenu/Effect/Card/Exhaust")]
  public class EffectExhaust : GameEffectSO
  {
    public int amount;
    public override void Execute(Unit user, Unit target, BattleManager manager)
    {
      if (target is not null)
      {
        
        Debug.Log($"[Damage Effect][{user.name} is Damage {target.name}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][타겟이 존재하지 않습니다.]");
      }
    }
  }  
}