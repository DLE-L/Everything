using UnityEngine;
using Utils;
namespace GameSystems.Scene.Game
{
  public class MapNodeUI : MonoBehaviour
  {   

    public void SetNodeUI(MapNode node)
    {
      var image = GetComponent<SpriteRenderer>();
      switch (node.NodeType)
      {
        case NodeType.Battle:
          image.color = Color.gray;
          break;
        case NodeType.Elite:
          image.color = Color.magenta;
          break;
        case NodeType.Event:
          image.color = Color.yellow;
          break;
        case NodeType.Shop:
          image.color = Color.green;
          break;
        case NodeType.Rest:
          image.color = Color.blue;
          break;
        case NodeType.Boss:
          image.color = Color.red;
          break;
      }
    }
  }
}