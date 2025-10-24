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
    
    public async Task LoadSceneTitleAsync() => await LoadSceneAsync(SceneType.Title);
    public async Task LoadSceneLobbyAsync() => await LoadSceneAsync(SceneType.Lobby, unloadMapScene:true);
    public async Task LoadSceneMapAsync() => await LoadSceneAsync(SceneType.Map);
    public async Task LoadSceneBattleAsync() => await LoadSceneAsync(SceneType.Battle);
    
    private async Task LoadSceneAsync(SceneType sceneType, bool unloadMapScene = false)
    {
      // 페이드 아웃
      
      // 다음 씬 로드
      var sceneName = GetSceneName(sceneType);
      if (sceneName is null)
      {
        Debug.LogWarning($"{sceneType} not found");
      }
      var loadHandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
      await loadHandle.Task;
      
      if (loadHandle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogWarning($"Failed to load scene '{sceneName}'");
      }

      // 이전 씬 언로드 & 특수 상황 처리
      if (!_currentAdditiveHandle.IsValid())
      {
        _ = SceneManager.UnloadSceneAsync("0_Loading");
      }
      
      if (sceneType != SceneType.Battle && _currentAdditiveHandle.IsValid())
      {
        _ = Addressables.UnloadSceneAsync(_currentAdditiveHandle).Task;
        _currentAdditiveHandle = default;
      }

      if (unloadMapScene && _mapSceneHandle.IsValid())
      {
        _ = Addressables.UnloadSceneAsync(_mapSceneHandle).Task;
        _mapSceneHandle = default;
      }

      if (sceneType == SceneType.Map)
      {
        _mapSceneHandle = loadHandle;
      }
      else
      {
        _currentAdditiveHandle = loadHandle;
      }

      // 4. 페이드 인
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