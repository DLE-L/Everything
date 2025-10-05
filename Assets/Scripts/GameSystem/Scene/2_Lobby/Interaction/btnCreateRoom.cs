using UnityEngine;
using Utils;
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Lobby
{
  public class btnCreateRoom : MonoBehaviour
  {
    private void OnClick(PointerEventData data)
    {
      var network = FindAnyObjectByType<NetWorkPhoton>();
      network.CreateRoom();
      Debug.Log($"[CreateRoom]");
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