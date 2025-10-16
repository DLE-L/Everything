using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Reward;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UIs.Common;
using Utils;

namespace UIs.Map
{
  public class Button_Narrative_Choice : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _choiceText;
    private NarrativeChoice _narrativeChoice;

    void Awake()
    {
      _choiceText ??= GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetChoice(NarrativeChoice narrativeChoice)
    {
      _narrativeChoice = narrativeChoice;
      _choiceText.text = narrativeChoice.Description;
    }
    

    private void OnClick(PointerEventData data)
    {
      RewardData reward = new(_narrativeChoice.Reward);
      SystemEvent.RaiseGrantsReward(reward);
      GameSystem.Instance.Map.mapUIManager.CloseCurrentCanvas();
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }

    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
    }

    private void OnDestroy()
    {
      Debug.Log($"{gameObject.name}: OnDestroy & Release");
      AssetLoader.ReleaseInstance(gameObject);
    }
  }
}