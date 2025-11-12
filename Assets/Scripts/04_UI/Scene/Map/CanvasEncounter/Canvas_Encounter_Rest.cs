using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using GamePlay.Map;
using Data.Act.Encounter;

namespace UIs.Map
{
  public class Canvas_Encounter_Rest : CanvasEncounterBase
  {
    [SerializeField] private TextMeshProUGUI _txtEventName;
    [SerializeField] private Transform _choiceRoot;
    [SerializeField] private List<btnRestOption> _restChoices;
    
    private Canvas _canvas;

    private void Awake()
    {
      _choiceRoot ??= GetComponentInChildren<HorizontalLayoutGroup>().transform;
    }

    private void Start()
    {
      _restChoices.ForEach(choice => choice.gameObject.SetActive(false));
    }

    public override Task SettingUIAsync(Node node)
    {
      if (node.Encounter is not EncounterRest rest) { Debug.Log($"Encounter is not Rest"); return null; }

      _txtEventName.text = rest.Name;
      
      for (var i = 0; i < rest.Options.Count; i++)
      {
        _restChoices[i].gameObject.SetActive(true);
        _restChoices[i].SetOption(rest.Options[i]);
      }

      return null; // TODO Canvas_Encounter_Narrative 반환 Task? 계속 쓸건지
    }
  }
}