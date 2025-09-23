using System;
using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class Node : MonoBehaviour
  {
    private GameManager _gameManager;
    private NodeInfo _info;
    public NodeType NodeType => _info.Type;

    void Awake()
    {
      _gameManager = GameSystem.Instance.Game;
    }

    public void SetNode(NodeInfo info)
    {
      _info = info;

      var renderer = GetComponent<SpriteRenderer>();
      if (info == null) { return; }

      renderer.color = info.Color; // TODO: Sprite변경

      NodeScript scriptType = info.Script;
      if (scriptType == null) Debug.LogError($"SetNode(NodeInfo info) info.Script?.GetType() == null");
      else
      {
        NodeScript nodeScript = gameObject.AddComponent<NodeScript>();
        nodeScript = scriptType;
        nodeScript.Init();
      }

    }
  }
}