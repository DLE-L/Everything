
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  [CreateAssetMenu(fileName = "NodeInfo", menuName = "MyMenu/NodeInfo")]
  public class NodeInfoSO : ScriptableObject
  {
    public NodeType NodeType;
    public Color NodeColor; // TODO: Sprite 변경
    public NodeScript NodeScript;
  }

  public class NodeInfo
  {
    public NodeType Type;
    public Color Color; // TODO: Sprite 변경
    public NodeScript Script;

    public NodeInfo(NodeInfoSO infoSO)
    {
      Type = infoSO.NodeType;
      Color = infoSO.NodeColor;
      Script = infoSO.NodeScript;
    }
    public NodeInfo(NodeInfo nodeInfo)
    {
      Type = nodeInfo.Type;
      Color = nodeInfo.Color;
      Script = nodeInfo.Script;
    }
  }

  public enum NodeType
  {
    Battle, Event,
    Elite, Shop, Rest,
    Boss
  }

  public abstract class NodeScript : MonoBehaviour, IEvent
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ChoiceButton> ChoiceList { get; set; }

    public virtual void SetEvent(NodeScript nodeScript)
    { 
      Name = nodeScript.Name;
      Description = nodeScript.Description;
      ChoiceList = new(nodeScript.ChoiceList);
    }
    public virtual void Init() { }
  }

  public interface IEvent
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ChoiceButton> ChoiceList { get; set; }
    public void Init();
  }
}