using System.Collections.Generic;
using Core;
using Core.Event;
using Data.Act;
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
    [SerializeField] private MapUIManager _uiManager;
    public void Awake()
    {
      GameSystem.Instance.RegisterGameManager(this);
      SystemEvent.RaiseOnStartNewRun();
      Debug.Log($"[New Run Start]");
    }

    public async void Start()
    {
      await CardDatabase.InitializeAsync();            
      _mapGenerateData = await AssetLoader.LoadAssetAsync<MapConfigSO>("GenerateMap_Data");

      _generator = new();
      await _generator.GenerateMap(_nodeParent, _mapGenerateData, 1);

      //_narrative_UI = _uiManager.GetComponentInChildren<Narrative_UI>();
      await _narrative_UI.Init();
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
        GameSystem.Instance.UnregisterGameManager();
      }
    }
  }
}