using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  public class MapGenerator
  {
    // 맵 생성 관련
    private const int ACT_FLOOR_COUNT = 15;
    private const int NODE_BOSS_INDEX = 14;
    private const int ACT_FINAL_ZONE_INDEX = 13;
    private const int ACT_START_ZONE_END_INDEX = 3;
    private const int NODE_MIN_DISTANCE = 2;
    // 노드 최대 개수
    private const int NODE_SHOP_MAX_COUNT = 2;
    private const int NODE_REST_MAX_COUNT = 2;
    private const int NODE_ELITE_MAX_COUNT = 3;
    // 노드 배치 관련
    private const int NODE_RANDOM_RANGE = 1;
    private const int NODE_DISTANCE = 3;
    private const int FLOOR_MAX_NODE = 3;
    private const int FLOOR_MIN_NODE = 2;

    private Dictionary<NodeType, int> _nodeTypeCountData = new();
    private Dictionary<NodeType, int[]> _specialNodePos = new();
    private Dictionary<NodeType, int> _nodeTypeCount = new();
    private List<Node> _nodeList = new();
    private System.Random _random = new();
    private List<NodeType> _randNodeTypes = new();    
    public void Init()
    {
      _nodeTypeCountData = new()
      {
        { NodeType.Shop, NODE_SHOP_MAX_COUNT },
        { NodeType.Rest, NODE_REST_MAX_COUNT },
        { NodeType.Elite, NODE_ELITE_MAX_COUNT },
      };

      _specialNodePos = new()
      {
        { NodeType.Shop, new int[1] {-NODE_MIN_DISTANCE} },
        { NodeType.Rest, new int[1] {-NODE_MIN_DISTANCE} },
        { NodeType.Elite, new int[1] {-NODE_MIN_DISTANCE} },
      };

      _randNodeTypes = new() { NodeType.Battle, NodeType.Event ,NodeType.Shop, NodeType.Rest, NodeType.Elite };    
    }

    public List<Node> GenerateMap(GameObject nodePrefab)
    {
      NodeType finalZoneType = _random.Next(0, 2) == 0 ? NodeType.Rest : NodeType.Shop;
      _nodeTypeCountData[finalZoneType]--;

      for (int floorIndex = 0; floorIndex < ACT_FLOOR_COUNT; floorIndex++)
      {
        int nodeCountOnFloor = (floorIndex == NODE_BOSS_INDEX) ? 1 : _random.Next(FLOOR_MIN_NODE, FLOOR_MAX_NODE + 1);

        for (int nodeIndex = 0; nodeIndex < nodeCountOnFloor; nodeIndex++)
        {
          NodeType assignedType = SelectNodeTypeForFloor(floorIndex, nodeIndex, finalZoneType);

          Vector2 position = SetNodePosition(floorIndex, nodeIndex, nodeCountOnFloor);
          Node mapNode = MonoBehaviour.Instantiate(nodePrefab, position, Quaternion.identity).GetComponent<Node>();

          mapNode.Data = new(assignedType, position);
          mapNode.name = (assignedType == NodeType.Boss) ? "Boss" : $"Node_{floorIndex}-{nodeIndex}_{assignedType}";
          mapNode.SetNodeData(mapNode.Data);
          mapNode.SetNode();

          _nodeList.Add(mapNode);
          _nodeTypeCount[assignedType] = _nodeTypeCount.GetValueOrDefault(assignedType, 0) + 1;

          if (_specialNodePos.ContainsKey(assignedType))
          {
            _specialNodePos[assignedType][0] = floorIndex;
          }
        }
      }
      
      return _nodeList;
    }

    private Vector2 SetNodePosition(int floorIndex, int nodeIndex, int totalNodesOnFloor)
    {      
      float GetRandomOffset() => ((float)_random.NextDouble() * 2 - 1.0f) * NODE_RANDOM_RANGE;

      // TODO: 중앙 정렬 등 필요 시 nodeIndex와 totalNodesOnFloor를 사용해 x 시작점을 조절할 수 있습니다.
      float xPos = nodeIndex * NODE_DISTANCE + GetRandomOffset();
      float yPos = floorIndex * NODE_DISTANCE + GetRandomOffset();

      return new Vector2(xPos, yPos);
    }

    private NodeType SelectNodeTypeForFloor(int floorIndex, int nodeIndex, NodeType finalZoneType)
    {
      if (floorIndex == NODE_BOSS_INDEX) return NodeType.Boss;
      if (floorIndex == ACT_FINAL_ZONE_INDEX && nodeIndex == 0) return finalZoneType;

      List<NodeType> nodeTypes = new(_randNodeTypes);

      if (floorIndex <= ACT_START_ZONE_END_INDEX)
      {
        nodeTypes.Remove(NodeType.Elite);
        nodeTypes.Remove(NodeType.Shop);
        nodeTypes.Remove(NodeType.Rest);
      }

      while (nodeTypes.Count > 0)
      {
        NodeType randType = nodeTypes[_random.Next(0, nodeTypes.Count)];

        int currentCount = _nodeTypeCount.GetValueOrDefault(randType, 0);
        int maxCount = _nodeTypeCountData.GetValueOrDefault(randType, int.MaxValue);

        if (currentCount >= maxCount)
        {
          nodeTypes.Remove(randType);
          continue;
        }

        if (_specialNodePos.TryGetValue(randType, out int[] lastPos)
        && Math.Abs(floorIndex - lastPos[0]) < NODE_MIN_DISTANCE)
        {
          nodeTypes.Remove(randType);
          continue;
        }

        return randType;
      }

      return _random.Next(0, 2) == 0 ? NodeType.Battle : NodeType.Event;
    }
  }
}

