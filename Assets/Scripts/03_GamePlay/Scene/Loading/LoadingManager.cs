using System;
using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace GamePlay.Loading
{
  public class LoadingManager : MonoBehaviour
  {
    private async void Start()
    {
      try
      {
        await GameSystem.Instance.Scene.LoadSceneTitleAsync();
      }
      catch (Exception e)
      {
        Debug.LogWarning($"LoadingManager-Start warning: {e.Message}");
      }
    }
  }
}