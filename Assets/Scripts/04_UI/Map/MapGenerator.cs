using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Utils;
using System.Linq;
using GamePlay.Map;
using Data.Map;
using Data.Act;
using Data.Act.Encounter;

namespace UI.Map
{
  public class MapGenerator
  {
    private System.Random _random = new System.Random();
    private List<Node> _generatedNodes = new List<Node>();
    private Dictionary<EncounterTypeSO, int> _typeCounts = new();
    private Dictionary<EncounterTypeSO, int> _lastSpawnFloors = new();
    private int _currentEliteCount = 0;

    public async Task<List<Node>> GenerateMap(Transform nodeParent, MapConfigSO mapConfig, int actNumbering)
    {
      _generatedNodes.Clear();
      _typeCounts.Clear();
      _lastSpawnFloors.Clear();
      _currentEliteCount = 0;

      ActSO actData = await ActDatabase.GetNumberingActAsync(actNumbering);
      GameObject nodePrefab = await AssetLoader.LoadAssetAsync<GameObject>("UI_Node");

      for (int floorIndex = 0; floorIndex < mapConfig.Act_FloorCount; floorIndex++)
      {        
        int nodeCountOnFloor;
        List<EncounterSO> encountersForThisFloor = new();

        if (floorIndex == mapConfig.Node_BossIndex) // 보스 층
        {
          nodeCountOnFloor = 1;
          encountersForThisFloor.Add(actData.BossEncounter);
        }
        else if (floorIndex == mapConfig.Act_FinalZoneIndex) // 보스 직전 층 (고정)
        {
          nodeCountOnFloor = 2;
          EncounterSO shopEncounter = actData.Encounters.FirstOrDefault(e => e.EncounterType == actData.ShopType);
          EncounterSO restEncounter = actData.Encounters.FirstOrDefault(e => e.EncounterType == actData.RestType);
          if (shopEncounter != null) encountersForThisFloor.Add(shopEncounter);
          if (restEncounter != null) encountersForThisFloor.Add(restEncounter);
          encountersForThisFloor = encountersForThisFloor.OrderBy(e => _random.Next()).ToList();
        }
        else
        {
          nodeCountOnFloor = _random.Next(mapConfig.Floor_MinNode, mapConfig.Floor_MaxNode + 1);
          for (int i = 0; i < nodeCountOnFloor; i++)
          {
            encountersForThisFloor.Add(SelectEncounterForFloor(floorIndex, mapConfig, actData));
          }
        }

        for (int nodeIndex = 0; nodeIndex < encountersForThisFloor.Count; nodeIndex++)
        {
          EncounterSO selectedEncounter = encountersForThisFloor[nodeIndex];
          if (selectedEncounter == null) continue;

          GameObject nodeGO = UnityEngine.Object.Instantiate(nodePrefab, nodeParent);
          RectTransform nodeRect = nodeGO.GetComponent<RectTransform>();

          Vector2 position = SetNodePosition(floorIndex, nodeIndex, encountersForThisFloor.Count, mapConfig);
          nodeRect.anchoredPosition = position;

          Node mapNode = nodeGO.GetComponent<Node>();
          mapNode.Setup(selectedEncounter);
          mapNode.name = $"Node_{floorIndex}-{nodeIndex}_{selectedEncounter.name}";
          _generatedNodes.Add(mapNode);

          var type = selectedEncounter.EncounterType;
          if (type != null) // 안전장치
          {
            _typeCounts[type] = _typeCounts.GetValueOrDefault(type, 0) + 1;
            _lastSpawnFloors[type] = floorIndex; // [수정] ContainsKey 체크 없이 바로 할당
          }
          if (selectedEncounter is EncounterCombat ce && ce.Rarity == actData.EliteRarity)
          {
            _currentEliteCount++;
          }
        }
      }

      return _generatedNodes;
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
            e.EncounterType == actData.ShopType ||
            e.EncounterType == actData.RestType ||
            (e is EncounterCombat ce && ce.Rarity == actData.EliteRarity)
        );
      }

      // 최대 개수 필터링
      candidatePool.RemoveAll(e => e.EncounterType != null && _typeCounts.GetValueOrDefault(e.EncounterType, 0) >= GetMaxCountForType(e.EncounterType, actData));
      if (_currentEliteCount >= actData.MaxEliteCount)
      {
        candidatePool.RemoveAll(e => e is EncounterCombat ce && ce.Rarity == actData.EliteRarity);
      }

      if (candidatePool.Count == 0) return null;

      // 가중치 기반 랜덤 선택
      int totalWeight = candidatePool.Sum(e => e.weight);
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
    private int GetMaxCountForType(EncounterTypeSO type, ActSO actData)
    {
      if (type == actData.ShopType) return actData.MaxShopCount;
      if (type == actData.RestType) return actData.MaxRestCount;
      return int.MaxValue;
    }
  }
}
