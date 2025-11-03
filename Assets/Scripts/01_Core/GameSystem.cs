using System;
using UnityEngine;
using GamePlay.Battle;
using GamePlay.Lobby;
using GamePlay.Map;
using GamePlay.Title;
using Core.Event;
using Data.Units;
using Utils;

namespace Core
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem Instance;
    public PlayerAccountData PlayerAccountData { get; private set; }

    #region Manager & System
    public SceneSystem Scene { get; } = new();

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
    }

    private async void Start()
    {
      try
      {
        PlayerRunAction.Init();
        PlayerAccountData = await PlayerDataManager.LoadAccountDataAsync();
        Debug.Log($"----GameSystem Initialized----");
      }
      catch (Exception e)
      {
        Debug.LogWarning($"GameSystem-Start warning: {e.Message}");
      }
    }
    public void SetNewAccountData(PlayerAccountData accountData) => PlayerAccountData = accountData;
    
    void OnDestroy()
    {
      PlayerRunAction.DeInit();
      AssetLoader.ReleaseAll();
    }
    
    #region Register Manager
    public void RegisterTitleManager(TitleManager manager) => Title = manager;
    public void UnregisterTitleManager() => Title = null;
    public void RegisterLobbyManager(LobbyManager manager) => Lobby = manager;
    public void UnregisterLobbyManager() => Lobby = null;
    public void RegisterMapManager(MapManager manager) => Map = manager;
    public void UnregisterMapManager() => Map = null;
    public void RegisterBattleManager(BattleManager manager) => Battle = manager;
    public void UnregisterBattleManager() => Battle = null;
    #endregion
  }
}