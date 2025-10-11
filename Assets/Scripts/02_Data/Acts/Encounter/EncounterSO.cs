using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Data.Map;
using Data.Reward;
using GamePlay.Map;
using UnityEngine.AddressableAssets;

namespace Data.Act.Encounter
{
  public abstract class EncounterSO : ScriptableObject
  {
    public NodeVisualsSO Visuals;
    public EncounterType Type;
    public List<RewardInfo> RewardInfos;
    public int weight = 100;

    public abstract Task BeginAsync(MapManager mapManager);
  }
  
  [Serializable]
  public class RewardInfo
  {
    public string Description;
    public RewardSO Reward;
  }
  public enum EncounterType
  {
    None,
    Narrative,
    Combat,    
    Shop,
    Rest,
    Boss,
  }
}