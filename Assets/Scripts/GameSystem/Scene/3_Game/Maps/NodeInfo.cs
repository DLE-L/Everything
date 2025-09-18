using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class NodeInfo : MonoBehaviour
  {
    public NodeManager NodeManger;

    void Awake()
    {
      NodeManger = GameObject.FindAnyObjectByType<NodeManager>();
    }

    public void SetNodeOfType(Node node)
    {
      var renderer = GetComponent<SpriteRenderer>();
      NodeInfoSO info = NodeManger.GetNodeInfo(node.NodeType);
      if (info == null)
      {
        return;
      }
      // TODO: Sprite변경
      renderer.color = info.NodeColor;
      if (info.SpecialComponent != null)
      {
        gameObject.AddComponent(info.SpecialComponent.GetType());
      }
    }
  }
}