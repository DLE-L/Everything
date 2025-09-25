
using System.Collections.Generic;
using UnityEngine;
using Units;
using System.Threading.Tasks;

namespace Item
{
  public abstract class TargetingStrategySO : ScriptableObject
  {
    public abstract Task<List<Unit>> FindTargetsAsync(Unit user, List<Unit> allAllies, List<Unit> allEnemies);
  }
}

/*
  public enum TargetType
  {
    Self,           // 자기 자신
    SingleEnemy,    // 적 하나 (플레이어 선택)
    AllEnemies,     // 모든 적
    RandomEnemy,    // 무작위 적 하나
    PlayerChoice,   // 손에 있는 카드 등, 플레이어가 특정 대상을 선택
    Attacker,       // 공격자
  }
*/