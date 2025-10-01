using UnityEngine;

namespace GameSystems.Scene.Lobby
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
      SystemEvent.OnClickNewRun += OnPlayerRunDeck;
    }
    void OnDisable()
    {
      SystemEvent.OnClickNewRun -= OnPlayerRunDeck;
    }
  }
}