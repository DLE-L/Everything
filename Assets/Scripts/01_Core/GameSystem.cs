using System;
using Utils;
using UnityEngine;
using GamePlay.Battle;
using GamePlay.Lobby;
using GamePlay.Map;
using GamePlay.Title;
using Data.Collectible.Card;
using Core.Event;
using Data.Act.Encounter;
using Data.Units;
using GamePlay.Units;

namespace Core
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public PlayerAccountData PlayerAccountData = new();
    public Player Player;
    
    public EncounterSO CurrentEncounter { get; set; }

    #region Manager & System
    private readonly SceneSystem _scene = new();
    public SceneSystem Scene => _scene;
    public TitleManager Title { get; private set; }
    public LobbyManager Lobby { get; private set; }
    public MapManager Map { get; private set; }
    public BattleManager Battle { get; private set; }
    #endregion

    private void Awake()
    {
      if (Instance is null)
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
      
      Debug.Log($"\\----GameSystem Initialized----//");
    }

    private async void Start()
    {
      try
      {
        await _scene.LoadSceneTitleAsync();
      }
      catch (Exception e)
      {
        Debug.LogError($"GameSystem Start Error: {e.Message}");
      }
    }

    public void RemoveCardFromDeckPermanently(CardSO cardToRemove)
    {
      var permanentDeck = Player.RunData.Deck;      
      if (permanentDeck.Remove(cardToRemove))
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
      SystemEvent.OnClickNode += OnClickNode;
    }

    void OnDisable()
    {
      SystemEvent.OnClickNode -= OnClickNode;
    }

    void OnDestroy()
    {
      AssetLoader.ReleaseAllAsset();
      AssetLoader.ReleaseAllInstance();
    }
  }
}