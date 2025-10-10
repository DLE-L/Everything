using Core;
using Core.Event;
using UnityEngine;

namespace GamePlay.Lobby
{
  public class LobbyManager : MonoBehaviour
  {

    void Awake()
    {
      GameSystem.Instance.RegisterLobbyManager(this);
    }

    public void OnPlayerRunDeck()
    {
      // GameSystem.Instance.PlayerRundDeckInitialize( TODO: 선택한 덱 할당 );
    }

    void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterLobbyManager();
      }
    }
    void OnEnable()
    {
      SystemEvent.OnStartNewRun += OnPlayerRunDeck;
    }
    void OnDisable()
    {
      SystemEvent.OnStartNewRun -= OnPlayerRunDeck;
    }
  }
}