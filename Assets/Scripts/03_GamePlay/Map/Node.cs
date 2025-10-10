using System;
using Data.Act.Encounter;
using Data.Map;
using UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace GamePlay.Map
{
  public class Node : MonoBehaviour
  {
    public EncounterTypeSO EncounterType { get; private set; }
    public EncounterSO Encounter { get; private set; }
    
    private Image _icon;

    void Awake()
    {
      _icon = GetComponent<Image>();
    }

    public void Setup(EncounterSO encounter)
    {      
      Encounter = encounter;
      EncounterType = encounter.EncounterType;
      //_icon.sprite = nodeData.Type.Icon;
      //GetComponent<SpriteRenderer>().sprite = nodeData.EncounterType.Icon;
    }

    private void OnClick(PointerEventData data)
    {
      EncounterType.BeginEncounter();
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