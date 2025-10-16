using System;
using System.Threading.Tasks;
using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Map;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace GamePlay.Map
{
  public class Node : MonoBehaviour
  {
    public EncounterSO Encounter { get; private set; }
    public EncounterNodeStyleSO EncounterNodeStyle { get; private set; }
    private Image _icon;

    void Awake()
    {
      _icon = GetComponent<Image>();
    }

    public void Setup(EncounterSO encounter)
    {
      Encounter = encounter;
      //_icon.sprite = nodeData.Type.Icon;
      //GetComponent<SpriteRenderer>().sprite = nodeData.EncounterType.Icon;
    }

    private async void OnClick(PointerEventData data)
    {
      try
      {
        SystemEvent.RaiseClickNode(this);
        await Encounter.BeginAsync(GameSystem.Instance.Map, this);
      }
      catch (Exception e)
      {
        Debug.Log($"[Node Error {e.Message}]");
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
}