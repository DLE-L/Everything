using System;
using Core;
using Core.Event;
using Data.Reward;
using UIs.Reward;
using UnityEngine;

namespace GamePlay.Reward
{
  public class RewardManager : MonoBehaviour
  {
    public RewardUIManager rewardUIManager;

    private void Awake()
    {
      GameSystem.Instance.ResisterRewardManager(this);
      rewardUIManager ??= FindFirstObjectByType<RewardUIManager>();
    }
    
    

    private void OnRewardPhaseStart(RewardStrategySO rewardStrategy)
    {
      rewardUIManager.ShowReward(rewardStrategy);
    }
    
    private void OnEnable()
    {
      BattleEvent.OnRewardPhaseStart += OnRewardPhaseStart;
    }
    private void OnDisable()
    {
      BattleEvent.OnRewardPhaseStart -= OnRewardPhaseStart;
    }

    private void OnDestroy()
    {
      if (GameSystem.Instance.Reward is not null)
      {
        GameSystem.Instance.UnregisterRewardManager();
      }
    }
  }
}