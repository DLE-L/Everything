using System;
using UnityEngine;

namespace Utils
{
  public enum NodeType
  {
    Battle, Event,
    Elite, Shop, Rest,
    Boss
  }

  [Serializable]
  public class NodeData
  {
    public NodeType Type;
    public Vector2 Pos;
    public NodeData(NodeType type, Vector2 pos)
    {
      Type = type;
      Pos = pos;
    }
  }  
}