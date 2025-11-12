using System;
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
  public class btnNarrativeChoice : MonoBehaviour
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

    private async void OnClick(PointerEventData data)
    {
      try
      {
        var rewardData = await _narrativeChoice.RewardStrategy.GenerateRewardAsync(); //TODO btnNarrativeChoice. Reward 방식 수정 필요
        var rewardResult = new RewardResult()
        {
          Cards = rewardData.CardsToPresent,
          Relics = rewardData.RelicsToPresent,
          Gold = rewardData.Gold,
        };
        SystemEvent.RaiseGrantsReward(rewardResult);
        GameSystem.Instance.Map.uiManager.CloseCurrentCanvas();
      }
      catch (Exception e)
      {
        Debug.LogWarning($"NarrativeChoice warning: {e.Message}");
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