using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utils
{
  public static class AssetLoader
  {
    private static Dictionary<string, AsyncOperationHandle> _handles = new(); // 딕셔너리<에셋 주소, 핸들>
    private static Dictionary<string, int> _refCounts = new(); // 딕셔너리<에셋 주소, 참조 카운트>

    public static async Task<IList<T>> LoadAssetLabelAsync<T>(string label) where T : Object
    {
      if (_handles.TryGetValue(label, out var handle))
      {
        _refCounts[label]++;
        return handle.Result as IList<T>;
      }

      var newHandle = Addressables.LoadAssetsAsync<T>(label);
      await newHandle.Task;

      StringBuilder sb = new();      
      if (newHandle.Status == AsyncOperationStatus.Succeeded)
      {
        _handles[label] = newHandle;
        _refCounts[label] = 1;
        // foreach (var asset in newHandle.Result)
        // {
        //   sb.AppendLine(asset.name);          
        // }
        // Debug.Log($"[AssetLoader Label Name]: {label}\n{sb.ToString()}");
        return newHandle.Result;
      }
      else
      {
        return null;
      }
    }

    public static async Task<T> LoadAssetAsync<T>(string assetAddress) where T : Object
    {
      if (_handles.TryGetValue(assetAddress, out var handle))
      {
        _refCounts[assetAddress]++;
        return handle.Result as T;
      }

      var newHandle = Addressables.LoadAssetAsync<T>(assetAddress);
      await newHandle.Task;

      if (newHandle.Status == AsyncOperationStatus.Succeeded)
      {
        _handles[assetAddress] = newHandle;
        _refCounts[assetAddress] = 1;
        //Debug.Log($"[AssetLoader]: {newHandle.Result.name}");
        return newHandle.Result;
      }
      else
      {
        return null;
      }
    }

    public static void ReleaseAsset(string assetAddress)
    {
      if (_handles.ContainsKey(assetAddress) == false)
      {
        return;
      }

      _refCounts[assetAddress]--;
      if (_refCounts[assetAddress] <= 0)
      {
        var handle = _handles[assetAddress];
        Addressables.Release(handle);

        _handles.Remove(assetAddress);
        _refCounts.Remove(assetAddress);
        Debug.Log($"[{assetAddress} Asset Released]");
      }
    }

    public static void ReleaseAllAsset()
    {
      List<string> address = new List<string>(_handles.Keys);
      foreach (var key in address)
      {
        ReleaseAsset(key);
      }
    }
  }
}