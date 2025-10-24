using System;
using Core;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Title
{
  public class btnContinueGame : MonoBehaviour
  {
    private async void OnClick(PointerEventData data)
    {
      try
      {
        await GameSystem.Instance.Scene.LoadSceneLobbyAsync();
      }
      catch (Exception e)
      {
        Debug.Log($"[{nameof(btnContinueGame)} Error: {e.Message}]");
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
