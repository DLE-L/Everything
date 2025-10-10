using Core;
using UnityEngine;

namespace GamePlay.Loading
{
  public class LoadingManager : MonoBehaviour
  {
    void Awake()
    {      
      //GameSystem.Instance.RegisterLobbyManager(this);
    }

    void OnDestroy()
    {

      if (GameSystem.Instance != null)
      {
        //GameSystem.Instance.UnregisterLobbyManager();
      }
    }
  }
}