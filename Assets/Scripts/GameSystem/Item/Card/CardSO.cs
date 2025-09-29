using System;
using UnityEngine;
using System.Collections.Generic;

namespace Item
{
  [Serializable]
  [CreateAssetMenu(fileName = "NewCard", menuName = "MyMenu/Item/Card")]
  public class CardSO : ScriptableObject
  {
    public string Name;
    public Sprite Illustration;
    public CardType Type;
    public CardRarity Rarity;
    public int Cost;
    public string Description;
    public bool Retain;
    public List<CardEffect> Effects;
  }

  [Serializable]
  public class CardEffect
  {
    public ItemEffectSO EffectType;
    public TargetingStrategySO Target;
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
}