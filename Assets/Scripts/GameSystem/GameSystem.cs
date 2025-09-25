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
    private LoadingManager _loading = new();
    private TitleManager _title = new();
    private LobbyManager _lobby = new();
    private GameManager _game = new();
    private BattleManager _battle = new();

    public LoadingManager Loading => _loading;
    public TitleManager Title => _title;
    public LobbyManager Lobby => _lobby;
    public GameManager Game => _game;
    public BattleManager Battle => _battle;
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
      _loading.Init();
      _title.Init();
      _lobby.Init();
      await _game.InitAsync();

      await CardDatabase.LoadCardData();

      // _scene.Init(); // TODO: 추후 다시 주석 해제
      NewGameStartAsync(); // TODO: 추후 다시 삭제 테스트용
    }

    void Update()
    {
      _game.UpdateGameManger();      

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

    // 계정 정보 -> 플레이 정보 변환
    private void ConvertAccountToRun(PlayerAccountData account, Player controller)
    {

    }

    // 플레이 정보 -> 계정 정보 변환
    private void ConvertRunToAccount(Player player)
    {

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