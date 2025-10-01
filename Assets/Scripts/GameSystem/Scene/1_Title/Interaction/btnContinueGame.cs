
using UnityEngine;
using Utils;
using System;
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Title
{
  public class btnContinueGame : MonoBehaviour
  {
    public void OnClickContinue(PointerEventData data)
    {
      SystemEvent.RaiseSceneLoadStart("2_Lobby");
    }
    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClickContinue;
    }
    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClickContinue;
    }
  }
}
