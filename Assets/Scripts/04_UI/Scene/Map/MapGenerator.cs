using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Utils;
using System.Linq;
using System.Text;
using Core;
using GamePlay.Map;
using Data.Act;
using Data.Act.Encounter;
using UnityEngine.AddressableAssets;

namespace UIs.Map
{
  public class MapGenerator
  {
    private readonly System.Random _random = new();
    private readonly Dictionary<EncounterType, int> _typeCounts = new();
    private readonly Dictionary<EncounterType, int> _lastSpawnFloors = new();
    private readonly List<List<Node>> _mapLayers = new();
    private int _currentEliteCount;
    private NodeSprite _nodeSprite;
    
    

    public async Task<List<List<Node>>> GenerateMap(AssetReference nodePrefabRef, Transform nodeRoot, MapConfigSO mapConfig,
      AssetReference actNumbering)
    {
      _typeCounts.Clear();
      _lastSpawnFloors.Clear();
      _mapLayers.Clear();
      _currentEliteCount = 0;

      _nodeSprite = GameSystem.Instance.Map.assetLoader.NodeSprite;

      var act = await AssetLoader.LoadAssetReferenceAsync<ActSO>(actNumbering);
      if (act is null)
      {
        Debug.LogError($"{actNumbering}에 해당하는 Act를 찾을 수 없습니다!");
        return null;
      }

      var encounterFixPoint = act.EncounterPoints
        .ToDictionary(point => (point.FloorIndex, point.NodeIndex), point => point.Encounter);

      int floorCount = mapConfig.Act_FloorCount;
      Dictionary<int, List<EncounterSO>> floorEncounters = new();
      List<Task<GameObject>> nodeTasks = new();

      #region PrepareFloorData

      for (int floorIndex = 0; floorIndex < floorCount; floorIndex++)
      {
        int nodeCountOnFloor = 0;
        List<EncounterSO> encountersFloor = new();
        if (floorIndex == mapConfig.Node_BossIndex)
        {
          nodeCountOnFloor = 1;
          encountersFloor.Add(act.BossEncounter);
        }
        else if (floorIndex == mapConfig.Act_FinalZoneIndex)
        {
          nodeCountOnFloor = 2;
          var shopEncounter = act.Encounters.FirstOrDefault(encounter => encounter.Type == EncounterType.Shop);
          var restEncounter = act.Encounters.FirstOrDefault(encounter => encounter.Type == EncounterType.Rest);
          if (shopEncounter is not null) encountersFloor.Add(shopEncounter);
          if (restEncounter is not null) encountersFloor.Add(restEncounter);
          encountersFloor = encountersFloor.OrderBy(e => _random.Next()).ToList();
        }
        else
        {
          nodeCountOnFloor = _random.Next(mapConfig.Floor_MinNode, mapConfig.Floor_MaxNode + 1);
          for (int i = 0; i < nodeCountOnFloor; i++)
          {
            var isExistEncounter = encounterFixPoint.TryGetValue((floorIndex + 1, i + 1), out var encounter);
            var encounterSo = isExistEncounter ? encounter : SelectEncounterForFloor(floorIndex, mapConfig, act);

            encountersFloor.Add(encounterSo);

            var type = encounterSo.Type;
            if (type is not EncounterType.None)
            {
              _typeCounts[type] = _typeCounts.GetValueOrDefault(type, 0) + 1;
              _lastSpawnFloors[type] = floorIndex;
            }

            if (encounterSo is EncounterCombat ce && ce.Rarity == act.EliteRarity)
            {
              _currentEliteCount++;
            }
          }
        }

        for (int nodeIndex = 0; nodeIndex < nodeCountOnFloor; nodeIndex++)
        {
          var nodeTask = AssetLoader.InstantiateAsync(nodePrefabRef, nodeRoot);
          nodeTasks.Add(nodeTask);
        }

        floorEncounters.Add(floorIndex, encountersFloor);
      }

      #endregion

      var nodePrefabs = await Task.WhenAll(nodeTasks);
      var nodePrefabQueue = new Queue<GameObject>(nodePrefabs);
      Debug.Log($"Node Queue : {nodePrefabQueue.Count}");

      #region SetFloorData

      for (int floorIndex = 0; floorIndex < floorCount; floorIndex++)
      {
        if (!floorEncounters.TryGetValue(floorIndex, out var nodeEncounters))
        {
          Debug.LogError($"MapGenerator-Floor Count Error");
          return null;
        }

        int nodeCount = nodeEncounters.Count;
        var currentLayerNodes = new List<Node>();

        for (int nodeIndex = 0; nodeIndex < nodeCount; nodeIndex++)
        {
          StringBuilder sb = new();
          var selectedEncounter = floorEncounters[floorIndex][nodeIndex];
          if (selectedEncounter is null) continue;

          var nodePrefab = nodePrefabQueue.Dequeue();
          var nodeRect = nodePrefab.GetComponent<RectTransform>();

          Vector2 position = SetNodePosition(floorIndex, nodeIndex, nodeCount, mapConfig);
          nodeRect.anchoredPosition = position;

          var mapNode = nodePrefab.GetComponent<Node>();
          selectedEncounter.Style.Icon = GetNodeSprite(selectedEncounter.Type);
          mapNode.Setup(selectedEncounter, floorIndex);

          sb.Append($"Node_{floorIndex}-{nodeIndex}_{selectedEncounter.name}");
          mapNode.name = sb.ToString();
          currentLayerNodes.Add(mapNode);
        }
        _mapLayers.Add(currentLayerNodes);
      }

      #endregion

      if (nodePrefabQueue.Count > 0)
      {
        Debug.LogWarning($"Node Prefab is more Instantiate : {nodePrefabQueue.Count}");
      }

      AssetLoader.ReleaseAssetByKey(actNumbering.AssetGUID);

      ConnectNodesRandomly();

      return _mapLayers;
    }
    
    private void ConnectNodesRandomly()
    {
      // 마지막 계층(보스) 직전까지만 연결
      for (int i = 0; i < _mapLayers.Count - 1; i++)
      {
        var currentLayer = _mapLayers[i];
        var nextLayer = _mapLayers[i + 1];

        if (nextLayer.Count == 0) continue;

        foreach (var node in currentLayer)
        {
          // Slay the Spire와 유사한 연결 (가까운 노드 위주)
          // 여기서는 간단히 1~2개의 랜덤 노드와 연결합니다.
          int connectionCount = _random.Next(1, 3); // 1 또는 2개 연결
            
          // 다음 층의 노드들을 섞어서 중복 없이 뽑기
          var shuffledNextLayer = nextLayer.OrderBy(n => _random.Next()).ToList();

          for (int j = 0; j < connectionCount && j < shuffledNextLayer.Count; j++)
          {
            node.nextNodes.Add(shuffledNextLayer[j]);
                
            // (선택) 라인 렌더러 등으로 시각적 연결
          }
        }
      }
    }

    private Vector2 SetNodePosition(int floorIndex, int nodeIndex, int totalNodesOnFloor, MapConfigSO generateData)
    {
      float totalWidth = (totalNodesOnFloor - 1) * generateData.Node_Distance;
      float startX = -totalWidth / 2f;
      float xPos = startX + nodeIndex * generateData.Node_Distance;

      float GetRandomOffset() => ((float)_random.NextDouble() * 2 - 1.0f) * generateData.Node_RandomRange;
      xPos += GetRandomOffset();

      float yPos = floorIndex * generateData.Node_Distance + GetRandomOffset();

      return new Vector2(xPos, yPos);
    }

    private EncounterSO SelectEncounterForFloor(int floorIndex, MapConfigSO mapConfig, ActSO actData)
    {
      List<EncounterSO> candidatePool = new(actData.Encounters);

      // 시작 구역 필터링
      if (floorIndex <= mapConfig.Act_StartZoneEndIndex)
      {
        candidatePool.RemoveAll(e =>
          e.Type == EncounterType.Shop ||
          e.Type == EncounterType.Rest ||
          (e is EncounterCombat ce && ce.Rarity == actData.EliteRarity)
        );
      }

      // 최대 개수 필터링
      candidatePool.RemoveAll(e => _typeCounts.GetValueOrDefault(e.Type, 0) >= GetMaxCountForType(e.Type, actData));
      if (_currentEliteCount >= actData.MaxEliteCount)
      {
        candidatePool.RemoveAll(e => e is EncounterCombat ce && ce.Rarity == actData.EliteRarity);
      }

      if (candidatePool.Count == 0) return null;

      // 가중치 기반 랜덤 선택
      int totalWeight = candidatePool.Sum(encounter => encounter.weight);
      if (totalWeight <= 0) return candidatePool.FirstOrDefault(); // 가중치가 모두 0인 경우 대비

      int randomValue = _random.Next(0, totalWeight);
      foreach (var encounter in candidatePool)
      {
        if (randomValue < encounter.weight)
        {
          return encounter;
        }

        randomValue -= encounter.weight;
      }

      return candidatePool.LastOrDefault(); // 만약의 경우 마지막 후보 반환
    }

    private int GetMaxCountForType(EncounterType type, ActSO actData)
    {
      return type switch
      {
        EncounterType.Shop => actData.MaxShopCount,
        EncounterType.Rest => actData.MaxRestCount,
        _ => int.MaxValue
      };
    }

    private Sprite GetNodeSprite(EncounterType encounterType)
    {
      return encounterType switch
      {
        EncounterType.Battle => _nodeSprite.Battle,
        EncounterType.Shop => _nodeSprite.Shop,
        EncounterType.Narrative => _nodeSprite.Narrative,
        EncounterType.Rest => _nodeSprite.Rest,
        EncounterType.Boss => _nodeSprite.Boss,
        _ => null
      };
    }
  }
}