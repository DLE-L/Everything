using System;
using System.Collections.Generic;
using Data.Collectible;
using Data.Effect;
using Data.Target;
using UnityEngine;

namespace Data.Collectible.Relic
{
  [CreateAssetMenu(fileName = "Relic_", menuName = "MyMenu/Relic/Relic")]
  public class RelicSO : CollectibleSO
  {
    public List<RelicEffect> Effects;
  }

  [Serializable]
  public class RelicEffect
  {
    public EffectTrigger Trigger;
    public GameEffectSO EffectType;
    public TargetingStrategySO Target;
  }

  public enum EffectTrigger
  {
    OnGameStart,        // 게임 시작 시
    OnCombatStart,      // 전투 시작 시
    OnTurnStart,        // 나의 턴 시작 시
    OnPlayerHit,        // 플레이어가 피격 당했을 때
    OnEnemyDeath,       // 적이 죽었을 때
    OnGoldGain          // 골드를 획득했을 때
  }
}