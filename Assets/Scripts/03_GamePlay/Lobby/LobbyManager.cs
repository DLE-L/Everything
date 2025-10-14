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

    void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterLobbyManager();
      }
    }
  }
}