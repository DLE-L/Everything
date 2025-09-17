using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Pipeline;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  public class MapGenerator
  {
    private const int ACT_FLOOR_COUNT = 15;     // 한 Act에 총 계층 수
    private const int BOSS_FLOOR_INDEX = 14;    // 보스 노드 인덱스
    private const int NODE_SHOP_MAX_COUNT = 2;  // 한 Act에 최대 상점 수
    private const int NODE_REST_MAX_COUNT = 2;  // 한 Act에 최대 휴식 수
    private const int NODE_ELITE_MAX_COUNT = 3; // 한 Act에 최대 엘리트 적 수
    private const int NODE_RANDOM_RANGE = 2;    // 노드 랜덤 범위
    private const int NODE_DISTANCE = 5;        // 노드 간 거리    
    private const int FLOOR_MAX_NODE = 3;       // 한 층에 최대 노드 수
    private const int FLOOR_MIN_NODE = 2;       // 한 층에 최소 노드 수

    private Dictionary<NodeType, int> _nodeTypeCountData = new();

    private Dictionary<NodeType, int> _nodeTypeCount = new();
    private List<MapNode> _nodeList = new();
    private System.Random _random = new();

    public List<MapNode> mapData = new();
    public void Init()
    {
      _nodeTypeCountData = new()
      {
        { NodeType.Shop, NODE_SHOP_MAX_COUNT },
        { NodeType.Rest, NODE_REST_MAX_COUNT },
        { NodeType.Elite, NODE_ELITE_MAX_COUNT },
      };
    }

    public void GenerateMap(GameObject nodePrefab)
    {
      _nodeList = new List<MapNode>();
      _nodeTypeCount = new(); // 노드 타입 카운터 초기화
      var nodeTypes = Enum.GetValues(typeof(NodeType));

      for (int floorIndex = 0; floorIndex < ACT_FLOOR_COUNT; floorIndex++)
      {
        int nodeCountOnFloor = (floorIndex == BOSS_FLOOR_INDEX) ?
        1 : (_random.Next(0, 2) == 0 ? FLOOR_MAX_NODE : FLOOR_MIN_NODE);

        for (int nodeIndex = 0; nodeIndex < nodeCountOnFloor; nodeIndex++)
        {
          // 1. 노드 생성
          Vector2 position = CalculateNodePosition(floorIndex, nodeIndex, nodeCountOnFloor);
          MapNode mapNode = MonoBehaviour.Instantiate(nodePrefab, position, Quaternion.identity).GetComponent<MapNode>();

          // 2. 노드 타입 결정
          NodeType assignedType;
          if (floorIndex == BOSS_FLOOR_INDEX)
          {
            assignedType = NodeType.Boss;
          }
          else
          {            
            NodeType randomType;
            do
            {
              int randomIndex = _random.Next(0, nodeTypes.Length - 1); // 보스는 제외
              randomType = (NodeType)nodeTypes.GetValue(randomIndex);              
            } while (IsMaxCountType(randomType));
            assignedType = randomType;
          }

          // 3. 데이터 설정 및 초기화
          mapNode.Data = new(assignedType, position);
          mapNode.name = (assignedType == NodeType.Boss) ? "Boss" : $"Node_{floorIndex}-{nodeIndex}_{assignedType}";
          mapNode.SetNodeUI();

          // 4. 리스트에 추가 및 로그
          _nodeList.Add(mapNode);
          LogSpecialNode(mapNode);
        }
      }
    }

    private Vector2 CalculateNodePosition(int floorIndex, int nodeIndex, int totalNodesOnFloor)
    {
      // -1.0 ~ 1.0 사이의 랜덤 값을 얻는 로직을 단순화합니다.
      float GetRandomOffset() => ((float)_random.NextDouble() * 2 - 1.0f) * NODE_RANDOM_RANGE;

      // TODO: 중앙 정렬 등 필요 시 nodeIndex와 totalNodesOnFloor를 사용해 x 시작점을 조절할 수 있습니다.
      float xPos = nodeIndex * NODE_DISTANCE + GetRandomOffset();
      float yPos = floorIndex * NODE_DISTANCE + GetRandomOffset();

      return new Vector2(xPos, yPos);
    }

    private void LogSpecialNode(MapNode node)
    {
      switch (node.Data.Type)
      {
        case NodeType.Shop:
        case NodeType.Rest:
        case NodeType.Elite:
          Debug.Log(node.name);
          break;
      }
    }

    private bool IsMaxCountType(NodeType type)
    {
      if (_nodeTypeCountData.TryGetValue(type, out int count) == false) return false;

      if (_nodeTypeCount.ContainsKey(type))
      {
        return _nodeTypeCount[type] >= count;
      }
      else
      {
        _nodeTypeCount[type] = 1;
        return false;
      }
    }
  }
}