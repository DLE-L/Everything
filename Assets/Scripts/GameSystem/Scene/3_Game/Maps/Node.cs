using UnityEngine;
using Utils;
using System.Collections.Generic;

namespace GameSystems.Scene.Game
{
  [RequireComponent(typeof(NodeInfo))]
  public class Node : MonoBehaviour
  {
    public NodeInfo NodeInfo;
    public NodeData Data;

    [SerializeField] public NodeType NodeType => Data.Type;
    [SerializeField] public Vector2 Pos => Data.Pos;
    //[SerializeField] public List<NodeData> children => Data.children;

    void Awake()
    {
      NodeInfo = GetComponent<NodeInfo>();
      gameObject.AddComponent<BoxCollider2D>();
    }

    public void SetNode()
    {
      NodeInfo.SetNodeOfType(this);
    }

    public void SetNodeData(NodeData data)
    {
      Data.Pos = data.Pos;
      Data.Type = data.Type;
    }
  }
}