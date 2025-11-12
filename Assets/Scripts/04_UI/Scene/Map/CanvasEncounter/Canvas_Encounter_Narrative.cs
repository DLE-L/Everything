using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Core;
using GamePlay.Map;
using Data.Act.Encounter;
using Utils;

namespace UIs.Map
{
  public class Canvas_Encounter_Narrative : CanvasEncounterBase
  {
    [SerializeField] private TextMeshProUGUI _txtEventName;
    [SerializeField] private TextMeshProUGUI _txtDescription;
    [SerializeField] private Transform _choiceRoot;
    [SerializeField] private List<btnNarrativeChoice> _narrativeChoices;
    
    private Canvas _canvas;

    private void Awake()
    {
      _choiceRoot ??= GetComponentInChildren<VerticalLayoutGroup>().transform;
    }

    private void Start()
    {
      _narrativeChoices.ForEach(choice => choice.gameObject.SetActive(false));
    }

    public override Task SettingUIAsync(Node node)
    {
      if (node.Encounter is not EncounterNarrative narrative) { Debug.Log($"Encounter is not Narrative"); return null; }

      _txtEventName.text = narrative.Name;
      _txtDescription.text = narrative.Description;
      
      for (var i = 0; i < narrative.Choices.Count; i++)
      {
        _narrativeChoices[i].gameObject.SetActive(true);
        _narrativeChoices[i].SetChoice(narrative.Choices[i]);
      }

      return null; // TODO Canvas_Encounter_Narrative 반환 Task? 계속 쓸건지
    }
  }
}