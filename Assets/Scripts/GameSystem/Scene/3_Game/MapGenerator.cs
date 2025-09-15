using System;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  public class MapGenerator
  {
    private System.Random _random = new();

    public void Init()
    {
      TestGenerateNode();
    }  

  public void TestGenerateNode()
    {
            var type = Enum.GetValues(typeof(NodeType));
      NodeType nodeType;
      for (int i = 0; i < 15; i++)
      {
        int num = _random.Next(0, 4);
        nodeType = (NodeType)type.GetValue(num);
        var node = new GameObject() { name = "node_" + i };
        node.gameObject.transform.position = new Vector3(0, i * 0.01f, 0);
        MapNode mapNode = node.AddComponent<MapNode>();
        if (i == 14)
        {
          node.name = "Boss";
          nodeType = NodeType.Boss;
        }
        mapNode.Data = new(nodeType, new Vector2(0, i));
      }
    }
  }
}