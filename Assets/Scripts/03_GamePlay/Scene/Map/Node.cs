using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Map;
using UIs.Common;
using UIs.Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace GamePlay.Map
{
  public class Node : MonoBehaviour
  {
    public EncounterSO Encounter { get; private set; }
    public int floorIndex { get; private set; }
    public EncounterNodeStyleSO EncounterNodeStyle => Encounter.Style;
    public List<Node> nextNodes = new();
    
    private Image _icon;
    
    private MapManager _mapManager;

    void Awake()
    {
      _icon = GetComponent<Image>();
      _mapManager = GameSystem.Instance.Map;
    }

    public void Setup(EncounterSO encounter, int floor)
    {
      Encounter = encounter;
      floorIndex = floor;
      
      _icon.sprite = EncounterNodeStyle.Icon;
      //GetComponent<SpriteRenderer>().sprite = nodeData.EncounterType.Icon;
    }

    private async void OnClick(PointerEventData data)
    {
      try
      {
        SystemEvent.RaiseClickNode(this);
        SystemEvent.RaiseEncounterEnter();
        await Encounter.BeginAsync(GameSystem.Instance.Map, this);
      }
      catch (Exception e)
      {
        Debug.LogWarning($"Node Onclick warning : {e.Message}");
      }
    }

    public void SetState(NodeState state)
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
      switch (state)
      {
        case NodeState.Accessible:
          UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
          _icon.color = Color.white;
          break;
        case NodeState.Inaccessible:
        case NodeState.Visited:
          UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
          _icon.color = Color.gray;
          break;
      }
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }

    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
    }
  }

  public enum NodeState
  {
    Accessible,
    Inaccessible,
    Visited,
  }
}