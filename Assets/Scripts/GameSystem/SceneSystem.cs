
using GameSystem.Scene.Loading;
using GameSystem.Scene.Title;
using GameSystem.Scene.Lobby;
using GameSystem.Scene.Game;
using GameSystem.Scene.Battle;
using UnityEngine.SceneManagement;

namespace GameSystem
{
  public class SceneSystem
  {
    #region Scenes    
    private static LoadingManager _loading = new();
    private static TitleManager _title = new();
    private static LobbyManager _lobby = new();
    private static GameManager _game = new();
    private static BattleManager _battle = new();

    public static LoadingManager Loading => _loading;
    public static TitleManager Title => _title;
    public static LobbyManager Lobby => _lobby;
    public static GameManager Game => _game;
    public static BattleManager Battle => _battle;
    #endregion

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