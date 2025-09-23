using System;
using UnityEngine;
using System.Collections.Generic;

namespace Item
{
  [Serializable]
  [CreateAssetMenu(fileName = "Card", menuName = "MyMenu/Card")]
  public class CardSO : ScriptableObject
  {
    public string Name;
    public Sprite Illustration;
    public CardType Type;
    public CardRarity Rarity;
    public int Cost;
    public string Description;
    public List<CardEffect> Effects;
  }

  [Serializable]
  public class CardEffect
  {
    public EffectType EffectType;
    public int Value;
    public TragetType Target;
  }

  public enum TragetType
  {
    Self,           // 자기 자신
    SingleEnemy,    // 적 하나 (플레이어 선택)
    AllEnemies,     // 모든 적
    RandomEnemy,    // 무작위 적 하나
    PlayerChoice    // 손에 있는 카드 등, 플레이어가 특정 대상을 선택
  }

  public enum CardRarity
  {
    Common,   // 일반
    Uncommon, // 고급
    Rare,     // 희귀
    Special   // 특수 (저주, 상태 등)
  }
  public enum CardType
  {
    Attack,  // 공격 카드
    Skill,   // 스킬 카드
    Power    // 파워 카드
  }
  public enum EffectType
  {
    //--- 기본 효과 ---
    Damage,         // 피해를 줌
    Block,          // 방어도를 얻음
    Draw,           // 카드를 뽑음
    Discard,        // 카드를 버림
    Heal,           // 체력을 회복함
    GainEnergy,     // 에너지를 얻음

    //--- 상태 이상 (Status) ---
    ApplyStatus,    // 대상에게 상태 이상을 부여함 (예: 약화, 취약)

    //--- 특수 효과 ---
    Exhaust,        // 카드를 소멸시킴 (전투에서 제외)
    UpgradeCard,    // 카드를 강화함
    GainGold,       // 골드를 얻음    
  }
}