using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Utils;
using System.Linq;
using System.Text;
using GamePlay.Map;
using Data.Map;
using Data.Act;
using Data.Act.Encounter;
using UnityEngine.AddressableAssets;

namespace UIs.Map
{
  public class MapGenerator
  {
    private readonly System.Random _random = new();
    private readonly List<Node> _generatedNodes = new();
    private readonly Dictionary<EncounterType, int> _typeCounts = new();
    private readonly Dictionary<EncounterType, int> _lastSpawnFloors = new();
    private int _currentEliteCount;

    public async Task<List<Node>> GenerateMap(AssetReference nodePrefabRef,Transform nodeParent, MapConfigSO mapConfig, int actNumbering)
    {
      _generatedNodes.Clear();
      _typeCounts.Clear();
      _lastSpawnFloors.Clear();
      _currentEliteCount = 0;
      
      ActSO actData = await ActDatabase.GetNumberingActAsync(actNumbering);
      Dictionary<(int, int), EncounterSO> encounterFixPoint = actData.EncounterPoints
        .ToDictionary(key => (key.FloorIndex, key.NodeIndex), value => value.Encounter);
      
      for (int floorIndex = 0; floorIndex < mapConfig.Act_FloorCount; floorIndex++)
      {        
        List<EncounterSO> encountersFloor = new();

        if (floorIndex == mapConfig.Node_BossIndex) // 보스 층
        {
          encountersFloor.Add(actData.BossEncounter);
        }
        else if (floorIndex == mapConfig.Act_FinalZoneIndex) // 보스 직전 층 (고정)
        {
          EncounterSO shopEncounter = actData.Encounters.FirstOrDefault(e => e.Type == EncounterType.Shop);
          EncounterSO restEncounter = actData.Encounters.FirstOrDefault(e => e.Type == EncounterType.Rest);
          if (shopEncounter is not null) encountersFloor.Add(shopEncounter);
          if (restEncounter is not null) encountersFloor.Add(restEncounter);
          encountersFloor = encountersFloor.OrderBy(e => _random.Next()).ToList();
        }
        else
        {
          var nodeCountOnFloor = _random.Next(mapConfig.Floor_MinNode, mapConfig.Floor_MaxNode + 1);
          
          for (int i = 0; i < nodeCountOnFloor; i++)
          {
            var isExistEncounter = encounterFixPoint.TryGetValue((floorIndex + 1, i + 1), out var encounter);
            var encounterSo = isExistEncounter ? encounter : SelectEncounterForFloor(floorIndex, mapConfig, actData);
            
            encountersFloor.Add(encounterSo);
          }
        }

        for (int nodeIndex = 0; nodeIndex < encountersFloor.Count; nodeIndex++)
        {
          StringBuilder sb = new();
          EncounterSO selectedEncounter = encountersFloor[nodeIndex];
          if (selectedEncounter is null) continue;

          var nodeGO = await AssetLoader.InstantiateAsync(nodePrefabRef, nodeParent);
          var nodeRect = nodeGO.GetComponent<RectTransform>();

          Vector2 position = SetNodePosition(floorIndex, nodeIndex, encountersFloor.Count, mapConfig);
          nodeRect.anchoredPosition = position;

          Node mapNode = nodeGO.GetComponent<Node>();
          mapNode.Setup(selectedEncounter);
          
          sb.Append($"Node_{floorIndex}-{nodeIndex}_{selectedEncounter.name}");
          mapNode.name = sb.ToString();
          _generatedNodes.Add(mapNode);

          EncounterType type = selectedEncounter.Type;
          if (type is not EncounterType.None) // 안전장치
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

    private int GetMaxCountForType(EncounterType type, ActSO actData)
    {
      return type switch
      {
        EncounterType.Shop => actData.MaxShopCount,
        EncounterType.Rest => actData.MaxRestCount,
        _ => int.MaxValue
      };
    }
  }
}
