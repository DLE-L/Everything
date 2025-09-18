using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  public class NodeManager : MonoBehaviour
  {
    public List<NodeInfoSO> nodeInfoSOs = new();

    private Dictionary<NodeType, NodeInfoSO> _infoMap = new();

    void Awake()
    {
      _infoMap = nodeInfoSOs.ToDictionary(info => info.NodeType);
    }

    public NodeInfoSO GetNodeInfo(NodeType type)
    {
      _infoMap.TryGetValue(type, out NodeInfoSO info);
      return info;
    }
  }
}