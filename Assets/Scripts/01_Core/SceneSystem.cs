using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
  public class SceneSystem
  {
    public async Task LoadSceneTitleAsync() => await LoadSceneAsync("1_Title");
    public async Task LoadSceneLobbyAsync() => await LoadSceneAsync("2_Lobby");
    public async Task LoadSceneMapAsync() => await LoadSceneAsync("3_Map");
    public async Task LoadSceneBattleAsync() => await LoadSceneAsync("4_Battle", LoadSceneMode.Additive);

    private async Task LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
      Debug.Log($"Scene Load: {sceneName}");
      await SceneManager.LoadSceneAsync(sceneName, mode);
    }
  }
}