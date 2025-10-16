using System;
using Utils;
using UnityEngine;
using GamePlay.Battle;
using GamePlay.Lobby;
using GamePlay.Map;
using GamePlay.Title;
using Core.Event;
using Data.Act.Encounter;
using Data.Units;
using GamePlay.Reward;

namespace Core
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public PlayerAccountData PlayerAccountData { get; private set; }
    public EncounterSO CurrentEncounter { get; set; }

    #region Manager & System

    public SceneSystem Scene { get; } = new();
    public RunSystem Run { get; private set; }

    public TitleManager Title { get; private set; }
    public LobbyManager Lobby { get; private set; }
    public MapManager Map { get; private set; }
    public RewardManager Reward { get; private set; }
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

      Debug.Log($"----GameSystem Initialized----");
    }

    private async void Start()
    {
      try
      {
        PlayerRunAction.Init();
        await Scene.LoadSceneTitleAsync();
      }
      catch (Exception e)
      {
        Debug.LogError($"GameSystem Start Error: {e.Message}");
      }
    }

    public void RegisterTitleManager(TitleManager manager) => Title = manager;
    public void UnregisterTitleManager() => Title = null;
    public void RegisterLobbyManager(LobbyManager manager) => Lobby = manager;
    public void UnregisterLobbyManager() => Lobby = null;
    public void RegisterMapManager(MapManager manager) => Map = manager;
    public void UnregisterMapManager() => Map = null;
    public void ResisterRewardManager(RewardManager manager) => Reward = manager;
    public void UnregisterRewardManager() => Reward = null;
    public void RegisterBattleManager(BattleManager manager) => Battle = manager;
    public void UnregisterBattleManager() => Battle = null;

    public void PlayerAccountDataInitialize(PlayerAccountData data) => PlayerAccountData = data;

    private void OnBeforeStartNewRun(PlayerRunData data)
    {
      Run = new(data);
    }

    private void OnStartNewRun()
    {
      Run.Init();
    }

    private void OnEndRun()
    {
      
    }

    private void OnClickNode(Node node) => CurrentEncounter = node.Encounter;


    void OnEnable()
    {
      SystemEvent.OnBeforeStartNewRun += OnBeforeStartNewRun;
      SystemEvent.OnStartNewRun += OnStartNewRun;
      SystemEvent.OnClickNode += OnClickNode;
      SystemEvent.OnEndRun += OnEndRun;
    }

    void OnDisable()
    {
      SystemEvent.OnBeforeStartNewRun -= OnBeforeStartNewRun;
      SystemEvent.OnStartNewRun -= OnStartNewRun;
      SystemEvent.OnClickNode -= OnClickNode;
      SystemEvent.OnEndRun -= OnEndRun;
    }

    void OnDestroy()
    {
      AssetLoader.ReleaseAllAsset();
      AssetLoader.ReleaseAllInstance();
      PlayerRunAction.DeInit();
    }
  }
}