using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;
using System.Threading.Tasks;
using Core.Event;
using Data.Act.Encounter;

namespace UI.Reward
{
  public class RewardUIManager : MonoBehaviour
  {
    public AssetReference rewardCanvasRef;
    private GameObject _rewardCanvas;
    
    public async void ShowReward(EncounterSO encounter)
    {
      try
      {
        _rewardCanvas = await AssetLoader.InstantiateAsync(rewardCanvasRef);
      }
      catch (Exception e)
      {
        Debug.Log($"[RewardUIManager Error: {e.Message}]");
      }
    }

    public void CloseReward()
    {
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