using System;
using Core;
using UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _04_UI.Lobby
{
  public class btnStartRun : MonoBehaviour
  {
    private async void OnClick(PointerEventData data)
    {
      try
      {
        await GameSystem.Instance.Scene.LoadSceneMapAsync();
      }
      catch (Exception e)
      {
        Debug.Log(e);
      }
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