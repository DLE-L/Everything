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
    [SerializeField] private TextMeshProUGUI _txtEncounterName;
    [SerializeField] private TextMeshProUGUI _txtEncounterDescription;
    [SerializeField] private Transform _choiceRoot;

    private UnityEngine.Canvas _canvas;

    private void Awake()
    {
      _choiceRoot = GetComponentInChildren<HorizontalLayoutGroup>().transform;
    }

    public override async Task SettingUIAsync(Node node)
    {
      if (node.Encounter.Type is not EncounterType.Narrative) return;

      var spawnTasks = new List<Task<GameObject>>();
      if (node.Encounter is not EncounterNarrative narrative) { Debug.Log($"Narrative not found"); return; }

      _txtEncounterName.text = narrative.Name;
      _txtEncounterDescription.text = narrative.Description;
      
      for (var i = 0; i < narrative.Choices.Count; i++)
      {
        Task<GameObject> spawnTask = AssetLoader.InstantiateAsync(GameSystem.Instance.Map.assetLoader.buttonNarrativeChoiceRef, _choiceRoot);
        spawnTasks.Add(spawnTask);
      }

      var choiceInstances = await Task.WhenAll(spawnTasks);
      for (var i = 0; i < choiceInstances.Length; i++)
      {
        var instance = choiceInstances[i];
        var buttonChoice = instance.GetComponent<Button_Narrative_Choice>();

        buttonChoice.SetChoice(narrative.Choices[i]);
      }
    }
  }
}