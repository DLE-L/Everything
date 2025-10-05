using GameSystems.Scene.Loading;
using GameSystems.Scene.Lobby;
using GameSystems.Scene.Title;
using GameSystems.Scene.Game;
using GameSystems.Scene.Battle;
using Utils;
using UnityEngine;
using Units;
using Item;
using System.Collections.Generic;

namespace GameSystems
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public PlayerAccountData PlayerAccountData = new();
    public RunPlayer Player;
    public Dictionary<string, int> PlayerRunDeck { get; private set; }

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

      SystemEvent.RaiseGameSystemInit();
      Debug.Log($"[GameSystem Initialized]");
    }

    private async void Start()
    {
      // await CardDatabase.InitializeAsync(); // TODO: Test용도
      PlayerAccountData = await PlayerDataManager.GetAccountDataAsync();
      PlayerAccountData ??= await PlayerDataManager.NewAccountDataAsync();

       _scene.Init();
    }

    public void OnStartNewRun()
    {
      
    }

    public void PlayerRundDeckInitialize(Dictionary<string, int> deck)
    {
      PlayerRunDeck = deck;
    }

    public void RemoveCardFromDeckPermanently(CardSO cardToRemove)
    {
      var permanentDeck = PlayerRunDeck;      
      if (permanentDeck.ContainsKey(cardToRemove.name))
      {
        permanentDeck.Remove(cardToRemove.name);
        Debug.Log($"'{cardToRemove.Name}' card permanent deck remove");
      }
      else
      {
        Debug.LogError($"'{cardToRemove.Name}' card is null");
      }
    }

    public void RegisterTitleManager(TitleManager manager) => Title = manager;
    public void UnregisterTitleManager() => Title = null;
    public void RegisterLobbyManager(LobbyManager manager) => Lobby = manager;
    public void UnregisterLobbyManager() => Lobby = null;
    public void RegisterGameManager(GameManager manager) => Game = manager;
    public void UnregisterGameManager() => Game = null;
    public void RegisterBattleManager(BattleManager manager) => Battle = manager;
    public void UnregisterBattleManager() => Battle = null;

    void OnEnable()
    {
      SystemEvent.OnSceneLoadStart += Scene.LoadScene;
      SystemEvent.OnStartNewRun += OnStartNewRun;
    }

    void OnDisable()
    {
      SystemEvent.OnSceneLoadStart -= Scene.LoadScene;
      SystemEvent.OnStartNewRun -= OnStartNewRun;
    }

    void OnDestroy()
    {
      SystemEvent.RaiseGameSystemExit();
      AssetLoader.ReleaseAllAsset();
    }
  }
}