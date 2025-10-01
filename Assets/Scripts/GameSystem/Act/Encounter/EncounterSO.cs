using System.Collections.Generic;
using UnityEngine;
using Units;
using Item;

namespace GameSystems.Act.Encounter
{
  [CreateAssetMenu(fileName = "NewEncounter", menuName = "MyMenu/Act/Encounter")]
  public class EncounterSO : ScriptableObject
  {
    [Header("인카운터 분류")]
    public EncounterSO EncounterType;
    public SpawnType SpawnType;

    [Header("랜덤 가중치")]
    public int weight = 100;

    [Header("전투 관련 데이터")] // Type이 Combat일때만 사용
    public List<EnemySO> Enemies;

    [Header("보상 관련 데이터")]
    public List<CardSO> CardRewards;
    public List<RelicSO> RelicRewards;

    public int GoldMax, GoldMin;

    public int GoldRewards()
    {
      System.Random random = new();
      return random.Next(GoldMin, GoldMax);
    }

    public CardSO CardReward()
    {
      System.Random random = new();
      int rand = random.Next(0, CardRewards.Count);
      return CardRewards[rand];      
    }
  }

  public enum SpawnType
  {
    Random, // 일반적인 경우. 풀(Pool) 안에서 랜덤하게 등장
    Fixed,  // 고정. 특정 층, 특정 위치에 반드시 등장 (예: 액트 중간 보스)
    Unique, // 유니크. 게임 전체에서 단 한 번만 등장하는 랜덤 인카운터
  }
}