
using System;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Lobby
{
  public class btnCreateRoom : MonoBehaviour
  {
    public event Action OnClickCreateRoom;

    public void OnClickCreate()
    {
      var network = FindAnyObjectByType<NetWorkPhoton>();
      network.CreateRoom();
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
        {
          OnClickCreateRoom?.Invoke();
        };

      OnClickCreateRoom += OnClickCreate;
    }

    void OnDisable()
    {
      OnClickCreateRoom -= OnClickCreate;
    }
  }
}