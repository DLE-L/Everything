using System;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.Act;

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

    // 노드 배치 관련
    private const int NODE_RANDOM_RANGE = 1;
    private const int NODE_DISTANCE = 3;
    private const int FLOOR_MAX_NODE = 3;
    private const int FLOOR_MIN_NODE = 2;

    private List<Node> _nodeList = new();
    private System.Random _random = new();
    public void Init()
    {

    }

    public List<Node> GenerateMap(GameObject nodePrefab)
    {
      GameManager gameManager = GameSystem.Instance.Game;

      // 마지막 구역 설정(Rest or Shop)
      // NodeType finalZoneType = _random.Next(0, 2) == 0 ? NodeType.Rest : NodeType.Shop;
      // _nodeTypeCountData[finalZoneType]--;

      // for (int floorIndex = 0; floorIndex < ACT_FLOOR_COUNT; floorIndex++)
      // {
      //   // 한 층에 노드 개수 설정
      //   int nodeCountOnFloor = (floorIndex == NODE_BOSS_INDEX) ? 1 : _random.Next(FLOOR_MIN_NODE, FLOOR_MAX_NODE + 1);

      //   for (int nodeIndex = 0; nodeIndex < nodeCountOnFloor; nodeIndex++)
      //   {
      //     NodeType assignedType = SelectNodeTypeForFloor(floorIndex, nodeIndex, finalZoneType);

      //     Vector2 position = SetNodePosition(floorIndex, nodeIndex, nodeCountOnFloor);
      //     Node mapNode = UnityEngine.MonoBehaviour.Instantiate(nodePrefab, position, Quaternion.identity).GetComponent<Node>();
      //     NodeInfo info = new(NodeInfoDataBase.GetNodeInfo(assignedType));
      //     mapNode.name = (assignedType == NodeType.Boss) ? "Boss" : $"Node_{floorIndex}-{nodeIndex}_{assignedType}";
      //     mapNode.SetNode(info);

      //     _nodeList.Add(mapNode);
      //     _nodeTypeCount[assignedType] = _nodeTypeCount.GetValueOrDefault(assignedType, 0) + 1;

      //     if (_specialNodePos.ContainsKey(assignedType))
      //     {
      //       _specialNodePos[assignedType][0] = floorIndex;
      //     }
      //   }
      // }

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

    /// <summary>
    /// 노드 타입 결정
    /// </summary>
    /// <param name="floorIndex"></param>
    /// <param name="nodeIndex"></param>
    /// <param name="finalZoneType"></param>
    /// <returns></returns>
    // private NodeType SelectNodeTypeForFloor(int floorIndex, int nodeIndex, NodeType finalZoneType)
    // {
    //   if (floorIndex == NODE_BOSS_INDEX) return NodeType.Boss; // 보스 노드 제외
    //   if (floorIndex == ACT_FINAL_ZONE_INDEX && nodeIndex == 0) return finalZoneType; // 보스 조우 직전 첫 노드 제외

    //   List<NodeType> nodeTypes = new(_randNodeTypes);

    //   if (floorIndex <= ACT_START_ZONE_END_INDEX)
    //   {
    //     nodeTypes.Remove(NodeType.Elite);
    //     nodeTypes.Remove(NodeType.Shop);
    //     nodeTypes.Remove(NodeType.Rest);
    //   }

    //   while (nodeTypes.Count > 0)
    //   {
    //     NodeType randType = nodeTypes[_random.Next(0, nodeTypes.Count)];

    //     int currentCount = _nodeTypeCount.GetValueOrDefault(randType, 0);
    //     int maxCount = _nodeTypeCountData.GetValueOrDefault(randType, int.MaxValue);

    //     if (currentCount >= maxCount)
    //     {
    //       nodeTypes.Remove(randType);
    //       continue;
    //     }

    //     if (_specialNodePos.TryGetValue(randType, out int[] lastPos)
    //     && Math.Abs(floorIndex - lastPos[0]) < NODE_MIN_DISTANCE)
    //     {
    //       nodeTypes.Remove(randType);
    //       continue;
    //     }

    //     return randType;
    //   }

    //   return _random.Next(0, 2) == 0 ? NodeType.Battle : NodeType.Event;
    // }
  }
}

