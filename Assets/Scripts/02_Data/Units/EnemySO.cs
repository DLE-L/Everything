using UnityEngine;
using System.Collections.Generic;
using Data.Collectible.Card;
using Data.Rarity;

namespace Data.Units
{
  [CreateAssetMenu(fileName = "Enemy_", menuName = "MyMenu/Character/Enemy")]
  public class EnemySO : ScriptableObject
  {
    public string Name;
    public Sprite Sprite;
    public int MaxHP;
    public RaritySO Rarity;
    public List<CardSO> Deck;
    public EnemyAILogicType AILogic;
  }

  public enum EnemyAILogicType
  {
     //--- 기본 AI ---
    SimpleRepeat,   // 단순 반복
    SimpleRandom,   // 덱에 있는 카드 중 하나를 완전히 무작위로 선택한다. (가장 기본)
    Aggressive,     // 공격(Attack) 타입의 카드를 우선적으로 선택한다.
    AggressiveRandom,
    Defensive,      // 방어/보조(Skill) 타입의 카드를 우선적으로 선택한다.

    //--- 고급 AI ---
    PatternBased,   // 덱에 있는 카드 순서 그대로, 정해진 패턴에 따라 카드를 선택한다.
    Strategic       // 버프/디버프 카드를 먼저 사용한 뒤, 공격 카드를 사용하는 등 특정 전략을 따른다.
  }
}