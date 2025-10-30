using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Utils
{
  public class AddressableObjectPooler
  {
    private readonly AssetReference _assetRefToPool;
    private readonly int _initialPoolSize;
    private readonly bool _allowGrowth;
    private readonly Transform _poolContainer;
    
    private readonly Queue<GameObject> _pool = new();
    private readonly List<GameObject> _allCreatedObjects = new();

    /// <summary>
    /// 오브젝트 풀을 생성합니다.
    /// </summary>
    /// <param name="assetRef">풀링할 프리팹.</param>
    /// <param name="initialSize">미리 생성할 개수.</param>
    /// <param name="allowGrowth">풀이 비었을 때 새로 생성할지 여부.</param>
    /// <param name="container">풀링된 오브젝트를 담을 부모 Transform (선택 사항).</param>
    public AddressableObjectPooler(AssetReference assetRef, int initialSize = 10, bool allowGrowth = true, Transform container = null)
    {
      if (assetRef == null)
      {
        Debug.LogError("ObjectPool 생성 실패: 프리팹은 null일 수 없습니다.");
        return;
      }

      _assetRefToPool = assetRef;
      _initialPoolSize = initialSize;
      _allowGrowth = allowGrowth;
      _poolContainer = container;

      Initialize();
    }
    
    private async void Initialize()
    {
      try
      {
        for (int i = 0; i < _initialPoolSize; i++)
        {
          await AddObjectToPool();
        }

        //Debug.Log($"[{_assetRefToPool}] 풀 초기화 완료 ({_pool.Count}개).");
      }
      catch (Exception e)
      {
        Debug.LogWarning($"AddressableObjectPooler:Initialize Error: {e.Message}");
      }
    }

    /// <summary>
    /// 새 오브젝트를 생성하고 비활성화 상태로 풀에 추가합니다.
    /// </summary>
    private async Task<GameObject> AddObjectToPool()
    {
      var instanceObject = await AssetLoader.InstantiateAsync(_assetRefToPool, _poolContainer);
      instanceObject.SetActive(false);
      _pool.Enqueue(instanceObject);
      _allCreatedObjects.Add(instanceObject);
      return instanceObject;
    }

    /// <summary>
    /// 풀에서 비활성화된 오브젝트를 가져옵니다. 없으면 새로 생성할 수 있습니다.
    /// </summary>
    /// <param name="parent">활성화 시 설정할 부모 (선택 사항).</param>
    /// <param name="position">설정할 위치 (선택 사항).</param>
    /// <param name="rotation">설정할 회전값 (선택 사항).</param>
    /// <returns>활성화된 게임 오브젝트 또는 null</returns>
    public async Task<GameObject> Get(Transform parent = null, Vector3? position = null, Quaternion? rotation = null)
    {
      GameObject objToGet = null;

      if (_pool.Count > 0)
      {
        objToGet = _pool.Dequeue();
      }
      else if (_allowGrowth)
      {
        //Debug.LogWarning($"풀({_assetRefToPool})이 비어 새 오브젝트를 생성합니다.");
        objToGet = await AddObjectToPool();
        _pool.Dequeue(); // 풀에 추가 후 바로 꺼냄
      }
      else
      {
        Debug.LogWarning($"풀({_assetRefToPool})이 비었고 성장이 허용되지 않아 null을 반환합니다.");
        return null;
      }
      
      if (parent is not null) objToGet.transform.SetParent(parent);
      else if (_poolContainer is not null) objToGet.transform.SetParent(null);

      if (position.HasValue) objToGet.transform.position = position.Value;
      if (rotation.HasValue) objToGet.transform.rotation = rotation.Value;

      objToGet.SetActive(true);

      (objToGet.GetComponent<IPoolableObject>())?.ResetState();

      return objToGet;
    }

    /// <summary>
    /// 사용이 끝난 오브젝트를 비활성화하고 풀에 반납합니다.
    /// </summary>
    /// <param name="obj">반납할 게임 오브젝트</param>
    public void Release(GameObject obj)
    {
      if (obj is null) return;
      
      obj.SetActive(false);
      obj.transform.SetParent(_poolContainer);

      _pool.Enqueue(obj);
    }

    /// <summary>
    /// 이 풀이 관리하는 모든 오브젝트(활성/비활성 포함)를 파괴합니다.
    /// 풀을 더 이상 사용하지 않을 때 (예: BattleManager.OnDestroy) 반드시 호출해야 합니다.
    /// </summary>
    public void Cleanup()
    {
      Debug.Log($"[{_assetRefToPool}] 풀 정리 중... 생성된 모든 오브젝트를 제거합니다.");
      foreach (GameObject obj in _allCreatedObjects)
      {
        if (obj is not null)
        {
          AssetLoader.ReleaseInstance(obj);
        }
      }

      _pool.Clear();
      _allCreatedObjects.Clear();
    }
  }
  
  /// <summary>
  /// 선택 사항: 풀링될 때 상태 리셋이 필요한 오브젝트를 위한 인터페이스.
  /// </summary>
  public interface IPoolableObject
  {
    /// <summary>
    /// Get()을 통해 풀에서 가져올 때 호출됩니다.
    /// </summary>
    void ResetState();
  }
}

