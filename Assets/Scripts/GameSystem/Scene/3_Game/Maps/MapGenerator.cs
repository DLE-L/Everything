using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  public class MapGenerator
  {
    private System.Random _random = new();

    public List<MapNode> mapData = new();

    public void Init()
    {

    }

    public void GenerateNode(GameObject MapNode)
    {
      var type = Enum.GetValues(typeof(NodeType));
      NodeType nodeType;
      for (int i = 0; i < 15; i++)
      {
        int num = _random.Next(0, 4);
        nodeType = (NodeType)type.GetValue(num);
        var node = MonoBehaviour.Instantiate(MapNode, new Vector3(0, i, 0), Quaternion.identity);
        node.name = $"Node_{i}_{nodeType.ToString()}";
        MapNode mapNode = node.GetComponent<MapNode>();
        if (i == 14)
        {
          node.name = "Boss";
          nodeType = NodeType.Boss;
        }
        mapNode.Data = new(nodeType, new Vector2(0, i));
        mapNode.SetNodeUI();
        mapData.Add(mapNode);
      }
    }
  }
}