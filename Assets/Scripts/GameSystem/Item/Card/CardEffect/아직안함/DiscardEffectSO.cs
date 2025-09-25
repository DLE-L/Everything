using Units;
using UnityEngine;
using GameSystems.Scene.Battle;
using GameSystems;

namespace Item.CardEffects
{
  [CreateAssetMenu(fileName = "CardEffect_Discard", menuName = "MyMenu/CardEffect/Discard")]
  public class DiscardEffectSO : ItemEffectSO
  {
    public int amount;
    public override void Execute(Unit user, Unit target)
    {
      if (user != null && user.tag == "Player")
      {
        //GameSystem.Instance.Battle.DiscardHandCardRandom();
        Debug.Log($"[Damage Effect][{user.name} is Damage {target.name}]");
      }
      else
      {
        Debug.Log($"[Damage Effect][타겟 {target.name}이 존재하지 않습니다.]");
      }
    }
  }  
}