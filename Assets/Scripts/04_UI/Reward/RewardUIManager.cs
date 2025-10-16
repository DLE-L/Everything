using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;
using Data.Reward;

namespace UIs.Reward
{
  public class RewardUIManager : MonoBehaviour
  {
    public AssetReference rewardCanvasRef;
    private GameObject _rewardCanvas;
    private Canvas_Reward_Combat rewardCombat;

    public async void ShowReward(RewardSO reward)
    {
      try
      {
        _rewardCanvas = await AssetLoader.InstantiateAsync(rewardCanvasRef);
        rewardCombat = _rewardCanvas.GetComponent<Canvas_Reward_Combat>();
        await rewardCombat.SetRewardData(reward);
        Debug.Log($"Show Reward: {reward}");
      }
      catch (Exception e)
      {
        Debug.Log($"[RewardUIManager ShowReward Error: {e.Message}]");
      }
    }

    public void CloseRewardCanvas()
    {
      rewardCombat.ReleaseRef();
      AssetLoader.ReleaseInstance(_rewardCanvas);
    }

    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
      
    }
  }
}