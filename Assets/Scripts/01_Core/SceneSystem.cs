using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Core
{
  public class SceneSystem
  {
    private AsyncOperationHandle _currentAdditiveHandle;
    private AsyncOperationHandle _mapSceneHandle;

    public async Task LoadSceneTitleAsync()
    {
      await LoadSceneAsync(SceneType.Title);
      _ = SceneManager.UnloadSceneAsync("0_Loading");
    }

    public async Task LoadSceneLobbyAsync() => await LoadSceneAsync(SceneType.Lobby, unloadMapScene:true);
    public async Task LoadSceneMapAsync() => await LoadSceneAsync(SceneType.Map);
    public async Task LoadSceneBattleAsync() 
    {
      var sceneName = GetSceneName(SceneType.Battle);
      var loadHandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
      await loadHandle.Task;

      if (loadHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogWarning($"Failed to load scene '{sceneName}'");
      }

      // Map 씬을 비활성화 (선택적)
      // if (_mapSceneHandle.IsValid()) _mapSceneHandle.Result.Scene.GetRootGameObjects()[0]?.SetActive(false);

      _currentAdditiveHandle = loadHandle;
    }
    
    public async Task ReturnToMapAsync()
    {
      // 페이드 아웃

      // 배틀 씬만 언로드합니다.
      if (_currentAdditiveHandle.IsValid())
      {
        await Addressables.UnloadSceneAsync(_currentAdditiveHandle).Task;
        _currentAdditiveHandle = default;
      }

      // Map 씬을 다시 활성화 (선택적)
      // if (_mapSceneHandle.IsValid()) _mapSceneHandle.Result.Scene.GetRootGameObjects()[0]?.SetActive(true);
    
      // 페이드 인
    }
    
    private async Task LoadSceneAsync(SceneType sceneType, bool unloadMapScene = false)
    {
      // 1. 페이드 아웃

      // 2. 새 씬 로드
      var sceneName = GetSceneName(sceneType);
      var loadHandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
      await loadHandle.Task;

      if (loadHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogWarning($"Failed to load scene '{sceneName}'");
        return;
      }

      // 3. 이전 씬들 언로드 (await로 기다림)
      // (이전 Title 또는 Lobby 씬)
      if (_currentAdditiveHandle.IsValid())
      {
        await Addressables.UnloadSceneAsync(_currentAdditiveHandle).Task;
      }
    
      // (맵 씬)
      if (unloadMapScene && _mapSceneHandle.IsValid())
      {
        await Addressables.UnloadSceneAsync(_mapSceneHandle).Task;
        _mapSceneHandle = default;
      }

      // 4. 새 핸들 저장
      if (sceneType == SceneType.Map)
      {
        _mapSceneHandle = loadHandle;
        _currentAdditiveHandle = default;
      }
      else
      {
        _currentAdditiveHandle = loadHandle;
      }

      // 5. 페이드 인
    }

    private string GetSceneName(SceneType sceneType)
    {
      switch (sceneType)
      {
        case  SceneType.Title:
          return "Scene_Title";
        case  SceneType.Lobby:
          return "Scene_Lobby";
        case  SceneType.Map:
          return "Scene_Map";
        case  SceneType.Battle:
          return "Scene_Battle";
        default:
          return null;
      }
    }

    private string GetSceneAssetLabel(SceneType sceneType)
    {
      switch (sceneType)
      {
        case  SceneType.Title:
          return "Title";
        case  SceneType.Lobby:
          return "Lobby";
        case  SceneType.Map:
          return "Map";
        case  SceneType.Battle:
          return "Battle";
        default:
          return null;
      }
    }
  }

  public enum SceneType
  {
    Loading,
    Title,
    Lobby,
    Map,
    Battle,
  }
}