using System;
using UnityEngine;
using GamePlay.Battle;
using GamePlay.Lobby;
using GamePlay.Map;
using GamePlay.Title;
using Core.Event;
using Data.Units;
using GamePlay.Reward;
using Utils;

namespace Core
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public PlayerAccountData PlayerAccountData { get; private set; }

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

    private void Start()
    {
      PlayerRunAction.Init();
    }

    public void RegisterTitleManager(TitleManager manager) => Title = manager;
    public void UnregisterTitleManager() => Title = null;
    public void RegisterLobbyManager(LobbyManager manager) => Lobby = manager;
    public void UnregisterLobbyManager() => Lobby = null;
    public void RegisterMapManager(MapManager manager) => Map = manager;
    public void UnregisterMapManager() => Map = null;
    public void RegisterBattleManager(BattleManager manager) => Battle = manager;
    public void UnregisterBattleManager() => Battle = null;

    public void PlayerAccountDataInitialize(PlayerAccountData data) => PlayerAccountData = data;

    private void OnBeforeStartNewRun(PlayerRunData data)
    {
      Run = new(data);
      SystemEvent.OnEndRun += Run.EndRun;
    }

    private void OnStartNewRun()
    {
      
    }
    
    private void OnClickNode(Node node) => Run.CurrentEncounter = node.Encounter;


    void OnEnable()
    {
      SystemEvent.OnBeforeStartNewRun += OnBeforeStartNewRun;
      SystemEvent.OnStartNewRun += OnStartNewRun;
      SystemEvent.OnClickNode += OnClickNode;
    }

    void OnDisable()
    {
      SystemEvent.OnBeforeStartNewRun -= OnBeforeStartNewRun;
      SystemEvent.OnStartNewRun -= OnStartNewRun;
      SystemEvent.OnClickNode -= OnClickNode;
      SystemEvent.OnEndRun -= Run.EndRun;
    }

    void OnDestroy()
    {
      PlayerRunAction.DeInit();
      AssetLoader.ReleaseAll();
    }
  }
}