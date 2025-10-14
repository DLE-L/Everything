using System;
using System.Collections.Generic;
using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Collectible.Card;
using Data.Units;
using GamePlay.Units;
using UI.Map;
using UnityEngine;
using Utils;
using Data.Reward;
using UnityEngine.AddressableAssets;

namespace GamePlay.Map
{
  public class MapManager : MonoBehaviour
  {
    private MapGenerator _generator;
    private MapConfigSO _mapGenerateData;
    private UI_Narrative _uiNarrative;
    [SerializeField] private Transform _nodeParent;
    [SerializeField] private AssetReference _nodePrefabRef;
    private List<RewardData>  _rewards;
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
        await _generator.GenerateMap(_nodePrefabRef, _nodeParent, _mapGenerateData, 1);

        //_narrative_UI = _uiManager.GetComponentInChildren<Narrative_UI>();
        _uiNarrative.Init();
      }
      catch (Exception e)
      {
        Debug.Log($"[MapManager Start Error: {e.Message}]");
      }
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