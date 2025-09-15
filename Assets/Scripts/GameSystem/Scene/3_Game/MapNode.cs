using UnityEngine;
using Utils;
using System.Collections.Generic;

namespace GameSystems.Scene.Game
{
  public class MapNode : MonoBehaviour
  {
    public NodeData Data;

    [SerializeField]public NodeType NodeType => Data.MapType;
    [SerializeField]public Vector2 Pos => Data.Pos;
    [SerializeField]public List<NodeData> children => Data.children;
  }
}