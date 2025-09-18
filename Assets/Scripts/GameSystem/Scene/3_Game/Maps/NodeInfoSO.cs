
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  [CreateAssetMenu(fileName = "NodeInfo", menuName = "MyMenu/NodeInfo")]
  public class NodeInfoSO : ScriptableObject
  {
    public NodeType NodeType;
    public Color NodeColor; // TODO: Sprite 변경
    public MonoBehaviour SpecialComponent;
  }
}