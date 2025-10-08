using UnityEngine;
using TMPro;
using System;
using Utils;
using Units;
using UnityEngine.EventSystems;
using GameSystems.Act;

namespace GameSystems.Scene.Game
{
  public class UI_Event_Choice : MonoBehaviour
  {
    [SerializeField] private GameObject _choiceTextObj;
    private TextMeshProUGUI _choiceText;
    private RewardSO _reward;
    void Awake()
    {
      _choiceText = _choiceTextObj.AddComponent<TextMeshProUGUI>();
      _choiceText.enableAutoSizing = true;
      _choiceText.alignment = TextAlignmentOptions.Center | TextAlignmentOptions.Midline;
    }

    public void SetChoice(RewardInfo info)
    {
      _reward = info.Reward;
      _choiceText.text = info.Description;      
    }

    private void OnClick(PointerEventData data)
    {
      SystemEvent.RaiseOnChoiceReward(_reward);
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