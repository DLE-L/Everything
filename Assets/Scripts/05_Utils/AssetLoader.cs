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
    private static readonly Dictionary<string, AsyncOperationHandle> _assetHandles = new(); // 딕셔너리<에셋 주소, 핸들>
    private static readonly List<GameObject> _spawnedInstances = new();

    public static async Task<IList<T>> LoadAssetsByLabelAsync<T>(string label) where T : Object
    {
      if (_assetHandles.TryGetValue(label, out var handle))
      {
        return handle.Result as IList<T>;
      }

      var newHandle = Addressables.LoadAssetsAsync<T>(label);
      await newHandle.Task;

      StringBuilder sb = new();
      if (newHandle.Status is not AsyncOperationStatus.Succeeded) return null;

      _assetHandles[label] = newHandle;
      // foreach (var asset in newHandle.Result)
      // {
      //   sb.AppendLine(asset.name);          
      // }
      // Debug.Log($"[AssetLoader Label Name]: {label}\n{sb.ToString()}");
      return newHandle.Result;
    }

    public static async Task<T> LoadAssetAsync<T>(string assetAddress) where T : Object
    {
      if (_assetHandles.TryGetValue(assetAddress, out var handle))
      {
        return handle.Result as T;
      }

      var newHandle = Addressables.LoadAssetAsync<T>(assetAddress);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded) return null;

      _assetHandles[assetAddress] = newHandle;
      //Debug.Log($"[AssetLoader]: {newHandle.Result.name}");
      return newHandle.Result;
    }

    public static async Task<GameObject> InstantiateAsync(string assetAddress, Vector3 position = default,
      Quaternion rotation = default, Transform parent = null)
    {
      var handle = Addressables.InstantiateAsync(assetAddress, position, rotation, parent);
      var instance = await handle.Task;
      _spawnedInstances.Add(instance);
      return instance;
    }

    public static async Task<GameObject> InstantiateAsync(AssetReference assetRef, Vector3 position = default,
      Quaternion rotation = default, Transform parent = null)
    {
      var handle = assetRef.InstantiateAsync(parent);
      var instance = await handle.Task;
      _spawnedInstances.Add(instance);
      return instance;
    }

    public static void ReleaseInstance(GameObject instance)
    {
      if (instance is not null && _spawnedInstances.Contains(instance))
      {
        Addressables.ReleaseInstance(instance);
        _spawnedInstances.Remove(instance);
      }
    }

    public static void ReleaseAsset(string assetAddress)
    {
      if (!_assetHandles.TryGetValue(assetAddress, out var handle)) return;

      Addressables.Release(handle);
      _assetHandles.Remove(assetAddress);

      Debug.Log($"[{assetAddress} Asset Released]");
    }

    public static void ReleaseAllAsset()
    {
      List<string> address = new(_assetHandles.Keys);
      foreach (var key in address)
      {
        ReleaseAsset(key);
      }
    }

    public static void ReleaseAllInstance()
    {
      foreach (var instance in _spawnedInstances)
      {
        Addressables.ReleaseInstance(instance);
      }
      _spawnedInstances.Clear();
    }
  }
}