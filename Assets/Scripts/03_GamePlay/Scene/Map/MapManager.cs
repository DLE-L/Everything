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
    [SerializeField] private Transform _nodeParent;
    
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

        _generator = new();
        await _generator.GenerateMap(assetLoader.NodePrefabRef, _nodeParent, _mapGenerateData, assetLoader.ActsRef[0]);

        AssetLoader.ReleaseAssetByKey(assetLoader.GenerateDataRef.AssetGUID);
      }
      catch (Exception e)
      {
        Debug.LogWarning($"MapManager Start warning: {e.Message}");
      }
    }

    private async void OnCombatEnd()
    {
      try
      {
        UnsubscribeBattleEvents();
        await GameSystem.Instance.Scene.LoadSceneMapAsync();
        //GameSystem.Instance.Map.mapUIManager.CanvasMapActive(true);
      }
      catch (Exception e)
      {
        Debug.LogError($"MapManager CombatEnd Error: {e.Message}");
      }
    }
    
    private void SubscribeBattleEvents()
    {
      BattleEvent.OnCombatEnd += OnCombatEnd;
    }

    private void UnsubscribeBattleEvents()
    {
      BattleEvent.OnCombatEnd -= OnCombatEnd;
    }

    private void OnEnable()
    {
      BattleEvent.OnCombatStart += SubscribeBattleEvents;
    }
    private void OnDisable()
    {
      BattleEvent.OnCombatStart -= SubscribeBattleEvents;
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