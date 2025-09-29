using UnityEngine;
using Units.Player;
using Utils;

namespace GameSystems.Scene.Lobby
{
  public class LobbyManager : MonoBehaviour
  {
    void Awake()
    {      
      GameSystem.Instance.RegisterLobbyManager(this);
    }

    void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterLobbyManager();
      }
    }  

    public void ContinueGame()
    {

    }

    public void NewGame()
    {

    }

    public async void NewGameStartAsync()
    {
      PlayerAccountData accountData = new();
      accountData.DefaultCardDeck();
      await JsonData.SavePlayerDataAsync(accountData);
      GameSystem.Instance.Player.Init(accountData);
    }

    public async void ContinueGameStartAsync()
    {
      GameSystem.Instance.Player.Init(await JsonData.LoadPlayerData());
    }
  }
}