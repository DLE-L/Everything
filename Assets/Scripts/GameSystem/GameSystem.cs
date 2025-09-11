
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

      // _scene.Init(); // TODO: 추후 다시 주석 해제
      CardDatabase.Init();
      NewGameStart(); // TODO: 추후 다시 삭제 테스트용
    }

    public async void NewGameStart()
    {
      PlayerAccountData accountData = new();
      accountData.DefaultCardDeck();
      await JsonData.SavePlayerDataAsync(accountData);
      PlayerData = accountData;      
    }

    public async void ContinueGameStart()
    {
      PlayerData = await JsonData.LoadPlayerData();
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