
using Utils;
using UnityEngine;
using Card;

namespace GameSystems
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public PlayerAccountData PlayerData { get; set; }

    private static SceneSystem _scene = new();

    public static SceneSystem Scene => _scene;


    private void Awake()
    {
      Init();
    }

    public void Init()
    {
      if (Instance == null)
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }

      _scene.Init();
      CardDatabase.Init();
    }
    
    public void LoadLobbyScene() => Scene.LoadSceneLobby();
    public void LoadGameScene() => Scene.LoadSceneGame();
    public void LoadBattleScene() => Scene.LoadSceneBattle();
    public void LoadTitleScene() => Scene.LoadSceneTitle();


    void OnDestroy()
    {
      AssetLoader.ReleaseAllAsset();
    }
  }
}