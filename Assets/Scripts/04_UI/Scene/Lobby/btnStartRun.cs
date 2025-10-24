using System;
using Core;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Lobby
{
  public class btnStartRun : MonoBehaviour
  {
    private async void OnClick(PointerEventData data)
    {
      try
      {
        GameSystem.Instance.Lobby.SettingBeforeNewRun();
        await GameSystem.Instance.Scene.LoadSceneMapAsync();
      }
      catch (Exception e)
      {
        Debug.Log($"Lobby-btnStartRun Error: {e.Message}");
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