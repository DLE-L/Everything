
using UnityEngine.SceneManagement;

namespace GameSystems
{
  public class SceneSystem
  {
    public void Init()
    {
      LoadSceneTitle();
    }

    public void LoadSceneTitle() => LoadScene("1_Title");
    public void LoadSceneLobby() => LoadScene("2_Lobby");
    public void LoadSceneGame() => LoadScene("3_Game");
    public void LoadSceneBattle() => LoadScene("4_Battle", LoadSceneMode.Additive);

    public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
      SceneManager.LoadSceneAsync(sceneName, mode);
    }    
  }
}