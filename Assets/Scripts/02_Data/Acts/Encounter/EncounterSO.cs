using System.Collections.Generic;
using UnityEngine;
using System;
using Data.Map;
using Data.Reward;

namespace Data.Act.Encounter
{
  public abstract class EncounterSO : ScriptableObject
  {
    public EncounterTypeSO EncounterType;
    public List<RewardInfo> Reward;
    public int weight = 100;

    public abstract void BeginEncounter();
  }
  
  [Serializable]
  public class RewardInfo
  {
    public string Description;
    public RewardSO Reward;
  }
}