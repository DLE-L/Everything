using System;
using Core;
using Core.Event;
using UIs.Map;
using UnityEngine;
using Utils;

namespace GamePlay.Map
{
  public class MapManager : MonoBehaviour
  {
    private MapGenerator _generator;
    private MapConfigSO _mapGenerateData;
    private Canvas_Encounter_Narrative _canvasEncounterNarrative;
    
    public MapUIManager uiManager;
    public MapAssetLoader assetLoader;
    
    public void Awake()
    {
      GameSystem.Instance.RegisterMapManager(this);
      uiManager ??= FindFirstObjectByType<MapUIManager>();
      assetLoader ??= FindFirstObjectByType<MapAssetLoader>();
      
      Debug.Log($"---New Run Start---");
      SystemEvent.RaiseStartNewRun();
    }

    public async void Start()
    {
      try
      {
        _mapGenerateData = await AssetLoader.LoadAssetReferenceAsync<MapConfigSO>(assetLoader.GenerateDataRef);
        await assetLoader.Init();

        _generator = new();
        await _generator.GenerateMap(assetLoader.NodePrefabRef, uiManager.nodeRoot, _mapGenerateData, assetLoader.ActsRef[0]);

        AssetLoader.ReleaseAssetByKey(assetLoader.GenerateDataRef.AssetGUID);
      }
      catch (Exception e)
      {
        Debug.LogWarning($"MapManager Start warning: {e.Message}");
      }
    }

    private async void OnBattleEnd()
    {
      try
      {
        UnsubscribeBattleEvents();
        await GameSystem.Instance.Scene.ReturnToMapAsync();
        SystemEvent.RaiseEncounterExit();
        //GameSystem.Instance.Map.mapUIManager.CanvasMapActive(true);
      }
      catch (Exception e)
      {
        Debug.LogError($"MapManager CombatEnd Error: {e.Message}");
      }
    }
    
    private void SubscribeBattleEvents()
    {
      BattleEvent.OnBattleEnd += OnBattleEnd;
    }

    private void UnsubscribeBattleEvents()
    {
      BattleEvent.OnBattleEnd -= OnBattleEnd;
    }

    private void OnEnable()
    {
      BattleEvent.OnBattleStart += SubscribeBattleEvents;
    }
    private void OnDisable()
    {
      BattleEvent.OnBattleStart -= SubscribeBattleEvents;
      UnsubscribeBattleEvents();
    }

    public void OnDestroy()
    {
      if (GameSystem.Instance.Map is not null)
      {
        GameSystem.Instance.UnregisterMapManager();
      }
    }
  }
}