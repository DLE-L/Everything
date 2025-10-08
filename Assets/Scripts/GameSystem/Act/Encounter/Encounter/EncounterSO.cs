using System.Collections.Generic;
using UnityEngine;
using System;
using Units;
using Item;

namespace GameSystems.Act
{
  public abstract class EncounterSO : ScriptableObject
  {
    public EncounterTypeSO EncounterType;
    public List<RewardInfo> Reward;
    public int weight = 100;

    public abstract void BeginEncounter();
  }

  public enum SpawnType
  {
    Random, // 일반적인 경우. 풀(Pool) 안에서 랜덤하게 등장
    Fixed,  // 고정. 특정 층, 특정 위치에 반드시 등장 (예: 액트 중간 보스)
    Unique, // 유니크. 게임 전체에서 단 한 번만 등장하는 랜덤 인카운터
  }
  
  [Serializable]
  public class RewardInfo
  {
    public string Description;
    public RewardSO Reward;
  }
}