using System.Collections.Generic;
using System.Linq;
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
    private static readonly Dictionary<GameObject, AsyncOperationHandle> _spawnedInstancesHandles = new(); // 딕셔너리<에셋 오브젝트, 핸들>
    private static readonly Dictionary<string, AsyncOperationHandle> _assetLabelsHandles = new(); 
    
    private static string GenerateKey(List<string> labels)
    {
      if (labels == null || labels.Count == 0)
      {
        return string.Empty;
      }
      var sb = new StringBuilder();
      var sortedLabels = labels.OrderBy(l => l);
      sb.AppendJoin("_", sortedLabels);
      return sb.ToString();
    }
    public static async Task<IList<T>> LoadAssetsByLabelsAsync<T>(List<string> labels) where T : Object
    {
      string key = GenerateKey(labels);
      
      if (_assetLabelsHandles.TryGetValue(key, out AsyncOperationHandle handle))
      {
        return handle.Result as IList<T>;
      }
      
      var newHandle = Addressables.LoadAssetsAsync<T>(labels, null,Addressables.MergeMode.Intersection);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"LoadAssetsByLabelsAsync failed: {key}");
        return null;
      }

      _assetLabelsHandles[key] = newHandle;
      return newHandle.Result;
    }

    public static async Task<IList<T>> LoadAssetsByLabelAsync<T>(string label) where T : Object
    {
      if (_assetHandles.TryGetValue(label, out var handle))
      {
        return handle.Result as IList<T>;
      }

      var newHandle = Addressables.LoadAssetsAsync<T>(label);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"LoadAssetsByLabelAsync failed: {label}");
        return null;
      }

      _assetHandles[label] = newHandle;
      // StringBuilder sb = new();
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

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"LoadAssetAsync failed: {assetAddress}");
        return null;
      }

      _assetHandles[assetAddress] = newHandle;
      Debug.Log($"AssetLoader: {newHandle.Result.name}");
      return newHandle.Result;
    }

    public static async Task<GameObject> InstantiateAsync(AssetReference assetRef, Vector3 position,
      Quaternion rotation, Transform parent = null)
    {
      var newHandle = assetRef.InstantiateAsync(position, rotation, parent);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"InstantiateAsync(AssetRef, Vector3, Quaternion, Transform) failed: {assetRef}");
        return null;
      }

      var instance = newHandle.Result;
      _spawnedInstancesHandles[instance] = newHandle;
      return instance;
    }

    public static async Task<GameObject> InstantiateAsync(AssetReference assetRef, Transform parent = null)
    {
      var newHandle = assetRef.InstantiateAsync(parent);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"InstantiateAsync(AssetRef, Transform) failed: {assetRef}");
        return null;
      }

      var instance = await newHandle.Task;
      _spawnedInstancesHandles[instance] = newHandle;
      return instance;
    }

    public static void ReleaseInstance(GameObject instance)
    {
      if (!_spawnedInstancesHandles.TryGetValue(instance, out var handle))
      {
        Debug.LogError($"{instance.name} was not spawned");
        return;
      }

      Addressables.ReleaseInstance(handle);
      _spawnedInstancesHandles.Remove(instance);
    }

    public static void ReleaseAsset(string assetAddress)
    {
      if (!_assetHandles.TryGetValue(assetAddress, out var handle))
      {
        Debug.LogError($"{assetAddress} was not loaded");
        return;
      }

      Addressables.Release(handle);
      _assetHandles.Remove(assetAddress);
    }

    public static void ReleaseAllInstance()
    {
      List<GameObject> objects = new(_spawnedInstancesHandles.Keys);
      foreach (var key in objects)
      {
        ReleaseInstance(key);
      }
    }

    public static void ReleaseAllAsset()
    {
      List<string> address = new(_assetHandles.Keys);
      foreach (var key in address)
      {
        ReleaseAsset(key);
      }
      List<string> labels = new(_assetLabelsHandles.Keys);
    }
  }
}