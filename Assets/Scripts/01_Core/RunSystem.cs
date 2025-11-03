using System;
using System.Collections.Generic;
using System.Linq;
using Core.Event;
using Data.Act.Encounter;
using Data.Collectible.Card;
using Data.Units;
using GamePlay.Map;
using UnityEngine;

namespace Core
{
  public class RunSystem : MonoBehaviour
  {
    public static RunSystem Instance;
    public PlayerRunData PlayerData { get; private set; }
    public EncounterSO CurrentEncounter { get; set; }
    
    private void Awake()
    {
      if (Instance is null)
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
    }

    public void Init(Dictionary<CardSO, int> deckDictionary)
    {
      PlayerData = PlayerDataManager.SetNewRunData(80, deckDictionary);
    }
    
    private void OnClickNode(Node node) => CurrentEncounter = node.Encounter;

    private async void EndRun()
    {
      try
      {
        await GameSystem.Instance.Scene.LoadSceneLobbyAsync();
        await PlayerDataManager.SaveAccountData(PlayerData);
      }
      catch (Exception e)
      {
        Debug.LogWarning($"RunSystem warning: {e.Message}");
      }
    }

    private void OnEnable()
    {
      SystemEvent.OnEndRun += EndRun;
      SystemEvent.OnClickNode += OnClickNode;
    }

    private void OnDisable()
    {
      SystemEvent.OnEndRun -= EndRun;
      SystemEvent.OnClickNode -= OnClickNode;
    }
  }
}