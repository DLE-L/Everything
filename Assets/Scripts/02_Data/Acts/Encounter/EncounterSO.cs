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
    public EncounterNodeStyleSO Style;
    public EncounterType Type;
    public RewardSO Reward;
    public int weight = 100;

    public abstract Task BeginAsync(MapManager mapManager);
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