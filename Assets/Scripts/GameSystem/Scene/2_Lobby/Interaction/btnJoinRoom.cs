using TMPro;
using Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Lobby
{
  public class btnJoinRoom : MonoBehaviour
  {
    public TMP_InputField roomId;

    private void OnClick(PointerEventData data)
    {
      var network = FindAnyObjectByType<NetWorkPhoton>();
      network.JoinRoom(roomId.text);
      Debug.Log($"[JoinRoom : {roomId.text}] ");
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }
    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
    }
  }
}