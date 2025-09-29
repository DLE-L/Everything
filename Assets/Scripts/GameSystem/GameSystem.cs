using Utils;
using UnityEngine;
using Units.Player;
using Item;
using GameSystems.Scene.Loading;
using GameSystems.Scene.Lobby;
using GameSystems.Scene.Title;
using GameSystems.Scene.Game;
using GameSystems.Scene.Battle;
using System.Collections;

namespace GameSystems
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public Player Player;

    private SceneSystem _scene = new();
    public SceneSystem Scene => _scene;

    #region Manager    
    public LoadingManager Loading { get; private set; }
    public TitleManager Title { get; private set; }
    public LobbyManager Lobby { get; private set; }
    public GameManager Game { get; private set; }
    public BattleManager Battle { get; private set; }
    #endregion

    private void Awake()
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
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private async void Start()
    {
      // _loading.Init();
      // _title.Init();
      // _lobby.Init();
      // await _game.InitAsync();

      await CardDatabase.InitializeAsync();

      // _scene.Init(); // TODO: 추후 다시 주석 해제
      NewGameStartAsync(); // TODO: 추후 다시 삭제 테스트용
    }

    public async void NewGameStartAsync() // TODO: 추후 다시 삭제 테스트용
    {
      PlayerAccountData accountData = new();
      accountData.DefaultCardDeck();
      await JsonData.SavePlayerDataAsync(accountData);
      Player?.Init(accountData);
    }

    public async void SavePlayerDataAsync()
    {
      await JsonData.SavePlayerDataAsync(Player.AccountData);
    }

    public void RegisterLobbyManager(LobbyManager manager) => Lobby = manager;
    public void UnregisterLobbyManager() => Lobby = null;
    public void RegisterTitleManager(TitleManager manager) => Title = manager;
    public void UnregisterTitleManager() => Title = null;
    public void RegisterGameManager(GameManager manager) => Game = manager;
    public void UnregisterGameManager() => Game = null;
    public void RegisterBattleManager(BattleManager manager) => Battle = manager;
    public void UnregisterBattleManager() => Battle = null;

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