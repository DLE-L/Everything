using System;
using UnityEngine;
using System.Collections.Generic;
using Units;
using System.Threading.Tasks;

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
    public List<CardEffect> Effects;
  }

  [Serializable]
  public class CardEffect
  {
    public ItemEffectSO EffectType;
    public TargetingStrategySO Target;

    public async Task UseCard(Unit user, List<Unit> allies, List<Unit> enemies)
    {
      List<Unit> targets = await Target.FindTargetsAsync(user, allies, enemies);
      foreach (var target in targets)
      {
        EffectType.Execute(user, target);
      }
    }
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