using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data.Collectible.Card;
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

    public Dictionary<CardSO, int> SelectDeck()
    {
      // TODO LobbyManager 선택 덱으로 변경
      return GameSystem.Instance.PlayerAccountData.DeckRecipes[1].
        ToDictionary(pair => CardDatabase.AllCards[pair.Key], pair => pair.Value);
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