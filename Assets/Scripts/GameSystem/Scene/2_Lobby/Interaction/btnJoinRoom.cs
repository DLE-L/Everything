using TMPro;
using Utils;
using UnityEngine;
using System;


namespace GameSystems.Scene.Lobby
{
  public class btnJoinRoom : MonoBehaviour
  {
    public TMP_InputField roomId;
    public event Action OnClickJoinRoom;
    
    public void OnClickJoin()
    {
      var network = FindAnyObjectByType<NetWorkPhoton>();
      network.CreateRoom();
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnClickJoinRoom?.Invoke();
      };

      OnClickJoinRoom += OnClickJoin;
    }
    void OnDisable()
    {
      OnClickJoinRoom -= OnClickJoin;
    }
  }
}