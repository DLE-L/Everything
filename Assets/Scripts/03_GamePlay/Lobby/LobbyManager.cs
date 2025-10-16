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
    public LobbyUIManager LobbyUIManager;
    void Awake()
    {
      GameSystem.Instance.RegisterLobbyManager(this);
      LobbyUIManager ??= FindFirstObjectByType<LobbyUIManager>();
    }

    private async void Start()
    {
      try
      {
        await LobbyUIManager.InitCanvasSceneAsync();
        await CardDatabase.InitializeAsync();
      }
      catch (Exception e)
      {
        Debug.Log($"LobbyManager Start error: {e.Message}");
      }
    }

    public void SettingBeforeNewRun()
    {
      var deck = GameSystem.Instance.PlayerAccountData.Decks["Deck_Default"].
                                   ToDictionary(item => CardDatabase.AllCards[item.Key], item => item.Value); // TODO: 추후 선택 할때로 변경
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