using Utils;
using UnityEngine;
using System.Collections.Generic;
using GamePlay.Battle;
using GamePlay.Lobby;
using GamePlay.Map;
using GamePlay.Title;
using Data.Card;
using Core.Event;
using Data.Act.Encounter;
using Data.Units;
using GamePlay.Units;
using UnityEngine.EventSystems;

namespace Core
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public PlayerAccountData PlayerAccountData = new();
    public Player Player;
    
    public EncounterSO CurrentEncounter { get; set; }
    public Dictionary<string, int> PlayerRunDeck { get; private set; }

    private readonly SceneSystem _scene = new();
    public SceneSystem Scene => _scene;

    #region Manager
    public TitleManager Title { get; private set; }
    public LobbyManager Lobby { get; private set; }
    public MapManager Map { get; private set; }
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

      _scene.LoadSceneTitle();
    }

    public void OnStartNewRun()
    {
      
    }

    public void PlayerRunDeckInitialize(Dictionary<string, int> deck)
    {
      PlayerRunDeck = deck;
    }

    public void RemoveCardFromDeckPermanently(CardSO cardToRemove)
    {
      var permanentDeck = PlayerRunDeck;      
      if (permanentDeck.Remove(cardToRemove.name))
      {
        Debug.Log($"'{cardToRemove.Name}' card permanent deck remove");
      }
      else
      {
        Debug.LogError($"'{cardToRemove.Name}' card is not existing");
      }
    }

    public void RegisterTitleManager(TitleManager manager) => Title = manager;
    public void UnregisterTitleManager() => Title = null;
    public void RegisterLobbyManager(LobbyManager manager) => Lobby = manager;
    public void UnregisterLobbyManager() => Lobby = null;
    public void RegisterMapManager(MapManager manager) => Map = manager;
    public void UnregisterMapManager() => Map = null;
    public void RegisterBattleManager(BattleManager manager) => Battle = manager;
    public void UnregisterBattleManager() => Battle = null;

    private void OnClickNode(Node node) => CurrentEncounter = node.Encounter;

    void OnEnable()
    {
      SystemEvent.OnStartNewRun += OnStartNewRun;
      SystemEvent.OnClickNode += OnClickNode;
    }

    void OnDisable()
    {
      SystemEvent.OnStartNewRun -= OnStartNewRun;
      SystemEvent.OnClickNode -= OnClickNode;
    }

    void OnDestroy()
    {
      SystemEvent.RaiseGameSystemExit();
      AssetLoader.ReleaseAllAsset();
      AssetLoader.ReleaseAllInstance();
    }
  }
}