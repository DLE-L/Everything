using System;
using System.Collections.Generic;
using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Card;
using Data.Units;
using GamePlay.Units;
using UI.Map;
using UnityEngine;
using Utils;
using Data.Reward;

namespace GamePlay.Map
{
  public class MapManager : MonoBehaviour
  {
    private MapGenerator _generator;
    private MapConfigSO _mapGenerateData;
    private Narrative_UI _narrative_UI;
    [SerializeField] private Transform _nodeParent;
    public MapUIManager UIManager;
    public void Awake()
    {
      GameSystem.Instance.RegisterMapManager(this);
      UIManager = GameObject.FindFirstObjectByType<MapUIManager>();
      
      SystemEvent.RaiseOnStartNewRun();
      Debug.Log($"[New Run Start]");
    }

    public async void Start()
    {
      try
      {
        await CardDatabase.InitializeAsync();            
        _mapGenerateData = await AssetLoader.LoadAssetAsync<MapConfigSO>("Data_GenerateMap");

        _generator = new();
        await _generator.GenerateMap(_nodeParent, _mapGenerateData, 1);

        //_narrative_UI = _uiManager.GetComponentInChildren<Narrative_UI>();
        _narrative_UI.Init();
      }
      catch (Exception e)
      {
        Debug.Log($"[MapManager 'Start' Exception: {e.Message}]");
      }
    }

    public void GetReward(RewardSO reward)
    {
      Player player = GameSystem.Instance.Player;
      PlayerRunData runData = player.RunData;

      // foreach (var card in reward.Cards)
      // {
      //   runData.Deck[card] = runData.Deck.GetValueOrDefault(card, 0) + 1;
      // }
      // foreach (var relic in reward.Relics)
      // {
      //   runData.Relics.Add(relic);
      // }
    }

    public void OnClickCombat(EncounterSO encounter)
    {
      GameSystem.Instance.CurrentEncounter = encounter;
    }

    void OnEnable()
    {
      SystemEvent.OnChoiceReward += GetReward;
    }
    void OnDisable()
    {
      SystemEvent.OnChoiceReward -= GetReward;
    }

    public void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterMapManager();
      }
    }
  }
}