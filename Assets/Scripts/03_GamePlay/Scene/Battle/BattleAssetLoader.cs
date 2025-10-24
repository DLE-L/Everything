using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GamePlay.Battle
{
  public class BattleAssetLoader : MonoBehaviour
  {
    private readonly Dictionary<string, AsyncOperationHandle> _assetHandles = new();
    private readonly List<AsyncOperationHandle> _assetInstanceHandles = new();

    public async Task Initialize()
    {
      
    }
    
    private void OnDestroy()
    {
      foreach (var handle in _assetHandles.Values)
      {
        Addressables.Release(handle);
      }
      _assetHandles.Clear();

      foreach (var handle in _assetInstanceHandles)
      {
        Addressables.ReleaseInstance(handle);
      }
      _assetInstanceHandles.Clear();
    }
  }
}