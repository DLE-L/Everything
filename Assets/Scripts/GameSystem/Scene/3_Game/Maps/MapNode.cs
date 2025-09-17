using UnityEngine;
using Utils;
using System.Collections.Generic;

namespace GameSystems.Scene.Game
{
  [RequireComponent(typeof(MapNodeUI))]
  public class MapNode : MonoBehaviour
  {
    public MapNodeUI NodeUI;
    public NodeData Data;

    [SerializeField] public NodeType NodeType => Data.Type;
    [SerializeField] public Vector2 Pos => Data.Pos;
    //[SerializeField] public List<NodeData> children => Data.children;

    void Awake()
    {
      NodeUI = GetComponent<MapNodeUI>();
    }

    public void SetNodeUI()
    {
      NodeUI.SetNodeUI(this);
    }
  }
}