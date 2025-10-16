using System;
using Core;
using Core.Event;
using Data.Reward;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Reward
{
  public class btnSelectReward : MonoBehaviour
  {
    private Canvas_Reward_Combat _combat;

    private void Awake()
    {
      _combat = FindFirstObjectByType<Canvas_Reward_Combat>();
    }

    private void OnClick(PointerEventData obj)
    {
      if (!_combat.IsCompleteSelection())
      {
        Debug.Log($"Please Select all reward ");
        return;
      } 
      RewardData rewardData = _combat.CompleteSelection();
      SystemEvent.RaiseGrantsReward(rewardData);
      GameSystem.Instance.Reward.rewardUIManager.CloseRewardCanvas();
    }
    
    private void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }

    private void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }
  }
}