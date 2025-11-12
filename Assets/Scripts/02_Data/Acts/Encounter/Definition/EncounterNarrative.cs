using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Reward;
using GamePlay.Map;
using UnityEngine;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Narrative_", menuName = "MyMenu/Act/Encounter/Narrative")]
  public class EncounterNarrative : EncounterSO
  {
    public string Name;
    public string Description;
    public List<NarrativeChoice> Choices;
    public override async Task BeginAsync(MapManager mapManager, Node node)
    {
      await mapManager.uiManager.ShowEncounter(mapManager.AssetLoader.NarrativeCanvasRef, node);
    }
  }

  [Serializable]
  public class NarrativeChoice
  {
    public string Description;
    public RewardStrategySO RewardStrategy; 
  }
}