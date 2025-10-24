using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using WebSocketSharp;

namespace Utils
{
  public static class AssetLoader
  {
    private static readonly Dictionary<string, AsyncOperationHandle> _singleAssetHandles = new();
    private static readonly Dictionary<string, AsyncOperationHandle> _labelListHandles = new();
    private static readonly Dictionary<string, AsyncOperationHandle> _multiLabelListHandles = new();
    private static readonly Dictionary<GameObject, AsyncOperationHandle> _spawnedInstanceHandles = new();
    
    public static void SceneReleaseAll()
    {
      var instances = new List<GameObject>(_spawnedInstanceHandles.Keys);
      foreach (var instance in instances)
      {
        ReleaseInstance(instance);
      }
      _spawnedInstanceHandles.Clear();
      
      var singleKeys = new List<string>(_singleAssetHandles.Keys);
      foreach (var key in singleKeys)
      {
        ReleaseAssetByKey(key);
      }
      _singleAssetHandles.Clear();

      
      var multiKeys = new List<string>(_multiLabelListHandles.Keys);
      foreach (var key in multiKeys)
      {
        ReleaseAssetByKey(key);
      }
      _multiLabelListHandles.Clear();
    }
    
    public static T GetAsset<T>(string key) where T : Object
    {
      if (_singleAssetHandles.TryGetValue(key, out var handle))
      {
        return handle.Result as T;
      }
      return null;
    }
    
    public static void ReleaseAssetByKey(string key)
    {
      bool released = false;
      if (_singleAssetHandles.TryGetValue(key, out var singleHandle))
      {
        Addressables.Release(singleHandle);
        //Debug.Log($"[AssetLoader] single asset [{key}] release");
        released = true;
      }
      else if (_labelListHandles.TryGetValue(key, out var labelHandle))
      {
        Addressables.Release(labelHandle);
        //Debug.Log($"[AssetLoader] single label [{key}] release");
        released = true;
      }
      else if (_multiLabelListHandles.TryGetValue(key, out var multiHandle))
      {
        Addressables.Release(multiHandle);
        //Debug.Log($"[AssetLoader] multi label [{key}] 해제 요청.");
        released = true;
      }

      if (!released)
      {
        Debug.LogWarning($"[AssetLoader] 해제하려는 키 [{key}]를 찾을 수 없습니다.");
      }
    }
    
    public static async Task<IList<T>> LoadAssetsByLabelsAsync<T>(List<string> labels) where T : Object
    {
      string key = GenerateKey(labels);
      if (_multiLabelListHandles.TryGetValue(key, out var handle))
      {
        return (IList<T>)handle.Result;
      }

      var newHandle = Addressables.LoadAssetsAsync<T>(labels, null, Addressables.MergeMode.Intersection);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"LoadAssetsByLabelsAsync failed: {key}");
        return null;
      }

      _multiLabelListHandles[key] = newHandle;
      return newHandle.Result;
    }
    
    public static async Task<IList<T>> LoadAssetsByLabelAsync<T>(string label) where T : Object
    {
      if (_labelListHandles.TryGetValue(label, out var handle))
      {
        return (IList<T>)handle.Result;
      }

      if (label.IsNullOrEmpty()) return null; 

      var newHandle = Addressables.LoadAssetsAsync<T>(label);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"LoadAssetsByLabelAsync failed: {label}");
        return null;
      }

      _labelListHandles[label] = newHandle;
      return newHandle.Result;
    }
    
    public static async Task<T> LoadAssetAsync<T>(string assetAddress) where T : Object
    {
      if (_singleAssetHandles.TryGetValue(assetAddress, out var handle))
      {
        return (T)handle.Result;
      }

      var newHandle = Addressables.LoadAssetAsync<T>(assetAddress);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"LoadAssetAsync failed: {assetAddress}");
        return null;
      }

      _singleAssetHandles[assetAddress] = newHandle;
      return newHandle.Result;
    }
    
    public static async Task<GameObject> InstantiateAsync(AssetReference assetRef, Vector3 position, Quaternion rotation,
      Transform parent = null)
    {
      var newHandle = assetRef.InstantiateAsync(position, rotation, parent);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"InstantiateAsync failed: {assetRef}");
        return null;
      }

      var instance = newHandle.Result;
      _spawnedInstanceHandles[instance] = newHandle;
      return instance;
    }

    public static async Task<GameObject> InstantiateAsync(AssetReference assetRef, Transform parent = null)
    {
      var newHandle = assetRef.InstantiateAsync(parent);
      await newHandle.Task;

      if (newHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"InstantiateAsync failed: {assetRef}");
        return null;
      }

      var instance = newHandle.Result;
      _spawnedInstanceHandles[instance] = newHandle;
      return instance;
    }
    
    public static void ReleaseInstance(GameObject instance)
    {
      if (instance is null) return;

      if (_spawnedInstanceHandles.TryGetValue(instance, out var handle))
      {
        Addressables.ReleaseInstance(handle);
        _spawnedInstanceHandles.Remove(instance);
      }
      else
      {
        // Addressable로 생성되지 않은 객체일 수 있으므로, 경고만 하고 파괴는 호출자가 결정
        Debug.LogWarning($"{instance.name} was not tracked by this AssetLoader.");
      }
    }

    public static void ReleaseAssetsByLabel(string label)
    {
      if (!_labelListHandles.TryGetValue(label, out var handle)) return;
      
      Addressables.Release(handle);
      _labelListHandles.Remove(label);
    }
    
    public static void ReleaseAll()
    {
      Debug.Log("[AssetLoader] Release All Handle");

      // 모든 인스턴스 해제
      var instances = new List<GameObject>(_spawnedInstanceHandles.Keys);
      foreach (var instance in instances)
      {
        ReleaseInstance(instance);
      }

      _spawnedInstanceHandles.Clear();

      // 로드했던 모든 '단일 에셋' 핸들 해제
      foreach (var handle in _singleAssetHandles.Values)
      {
        Addressables.Release(handle);
      }

      _singleAssetHandles.Clear();

      // 로드했던 모든 '단일 레이블' 핸들 해제
      foreach (var handle in _labelListHandles.Values)
      {
        Addressables.Release(handle);
      }

      _labelListHandles.Clear();

      // 로드했던 모든 '다중 레이블' 핸들 해제
      foreach (var handle in _multiLabelListHandles.Values)
      {
        Addressables.Release(handle);
      }

      _multiLabelListHandles.Clear();
    }

    public static string GenerateKey(List<string> labels)
    {
      if (labels is null || labels.Count is 0) return string.Empty;
      var sb = new StringBuilder();
      var sortedLabels = labels.OrderBy(l => l);
      sb.AppendJoin("_", sortedLabels);
      return sb.ToString();
    }
  }
}