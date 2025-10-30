using UnityEngine;
using GamePlay.Units;
using GamePlay.Battle;

namespace Data.Effect
{  
  public abstract class GameEffectSO : ScriptableObject
  {
    public abstract void Execute(Unit user, Unit target);
  }
}

/*
  public enum EffectType
  {
    //--- 기본 효과 ---
    Damage,         // 피해를 줌
    Block,          // 방어도를 얻음
    Draw,           // 카드를 뽑음
    Discard,        // 카드를 버림
    Heal,           // 체력을 회복함
    GainEnergy,     // 에너지를 얻음
    ModifyGoldGain,

    //--- 상태 이상 (Status) ---
    ApplyStatus,    // 대상에게 상태 이상을 부여함 (예: 약화, 취약)

    //--- 특수 효과 ---
    Exhaust,        // 카드를 소멸시킴 (전투에서 제외)
    UpgradeCard,    // 카드를 강화함
    GainGold,       // 골드를 얻음    
  }
*/