using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Event;
using UIs;
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
    
    private List<List<Node>> _mapLayers; // 생성된 맵의 계층 구조
    private Node _currentNode; // 플레이어의 현재 위치
    
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
        _mapLayers = await _generator.GenerateMap(assetLoader.NodePrefabRef, uiManager.nodeRoot, _mapGenerateData, assetLoader.ActsRef[0]);

        AssetLoader.ReleaseAssetByKey(assetLoader.GenerateDataRef.AssetGUID);
        InitializeMapState();
        
        await FadeManger.Instance.FadeIn();
      }
      catch (Exception e)
      {
        Debug.LogWarning($"MapManager Start warning: {e.Message}");
      }
    }
    
    private void InitializeMapState()
    {
      // 모든 노드를 일단 '접근 불가능'으로 설정
      foreach (var layer in _mapLayers)
      {
        foreach (var node in layer)
        {
          node.SetState(NodeState.Inaccessible);
        }
      }

      // 1계층 노드만 '접근 가능'으로 설정
      foreach (var node in _mapLayers[0]) // 첫 번째 계층
      {
        node.SetState(NodeState.Accessible);
      }
    }
    
    private void OnSelectNode(Node selectedNode)
    {
      if (_currentNode is not null)
      {
        foreach (var node in _currentNode.nextNodes)
        {
          node.SetState(NodeState.Visited);
        }
      }
      
      _currentNode = selectedNode;
      _currentNode.SetState(NodeState.Visited);

      int currentFloor = _currentNode.floorIndex;
      foreach (var node in _mapLayers[currentFloor])
      {
        if (node != _currentNode) // 방금 선택한 노드 제외
        {
          node.SetState(NodeState.Inaccessible);
        }
      }
      
      foreach (var connectedNode in _currentNode.nextNodes)
      {
        connectedNode.SetState(NodeState.Accessible);
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
      SystemEvent.OnClickNode += OnSelectNode;
      BattleEvent.OnBattleStart += SubscribeBattleEvents;
    }
    private void OnDisable()
    {
      BattleEvent.OnBattleStart -= SubscribeBattleEvents;
      SystemEvent.OnClickNode -= OnSelectNode;
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