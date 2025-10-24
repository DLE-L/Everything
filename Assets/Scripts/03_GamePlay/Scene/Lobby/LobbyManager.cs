using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Event;
using Data.Collectible.Card;
using Data.Units;
using UIs.Lobby;
using UnityEngine;

namespace GamePlay.Lobby
{
  public class LobbyManager : MonoBehaviour
  {
    public LobbyUIManager uiManager;
    public LobbyAssetLoader assetLoader;
    void Awake()
    {
      GameSystem.Instance.RegisterLobbyManager(this);
      uiManager ??= FindFirstObjectByType<LobbyUIManager>();
      assetLoader ??= FindFirstObjectByType<LobbyAssetLoader>();
    }

    private async void Start()
    {
      try
      {
        await CardDatabase.InitializeAsync();
      }
      catch (Exception e)
      {
        Debug.Log($"LobbyManager Start error: {e.Message}");
      }
    }

    public void SettingBeforeNewRun()
    {
      // TODO: 추후 선택 할때로 변경
      var deck = GameSystem.Instance.PlayerAccountData.Decks["Deck_Default"].
                                   ToDictionary(item => CardDatabase.AllCards[item.Key], item => item.Value); 
      var runData = PlayerDataManager.NewRunInitialize(80, deck);
      SystemEvent.RaiseBeforeStartNewRun(runData);
    }

    void OnDestroy()
    {
      if (GameSystem.Instance.Lobby is not null)
      {
        GameSystem.Instance.UnregisterLobbyManager();
      }
    }
  }
}