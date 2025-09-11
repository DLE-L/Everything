using System.Collections.Generic;
using Utils;

namespace GameSystems.Scene.Game
{
  public static class EncounterDatabase
  {
    public static Dictionary<string, EncounterSO> encounters = new();

    public static EncounterSO CurrentEncounter;

    public async static void LoadEncounterData()
    {
      var encounterList = await AssetLoader.LoadAssetLabelAsync<EncounterSO>("Encounter");
      foreach (var encounter in encounterList)
      {
        encounters.Add(encounter.name, encounter);        
      }
    } 
  }
}