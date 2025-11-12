using System;
using Core;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Map
{
  public class btnClose : MonoBehaviour
  {
    private void OnClick(PointerEventData obj)
    {
      GameSystem.Instance.Map.uiManager.CloseCurrentCanvas();
    }
    
    private void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }
    private void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
    }
  }
}