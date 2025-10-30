using System;
using Data.Act.Encounter;
using Data.Units;
using UnityEngine;

namespace Core
{
  public class RunSystem
  {
    public PlayerRunData PlayerData { get; private set; }
    public EncounterSO CurrentEncounter { get; set; }

    public RunSystem(PlayerRunData data)
    {
      PlayerData = data;
    }

    public async void EndRun()
    {
      try
      {
        await GameSystem.Instance.Scene.LoadSceneLobbyAsync();
      }
      catch (Exception e)
      {
        Debug.LogWarning($"RunSystem warning: {e.Message}");
      }
    }
  }
}