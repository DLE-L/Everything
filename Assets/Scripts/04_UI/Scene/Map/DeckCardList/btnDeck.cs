using System;
using Core;
using GamePlay.Map;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Map
{
  public class btnDeck : MonoBehaviour
  {
    [SerializeField] private MapManager _mapManager;

    private void Awake()
    {
      _mapManager ??= GameSystem.Instance.Map;
    }

    private void OnClick(PointerEventData obj)
    {
      if (_mapManager.uiManager.DeckCanvasObject is null)
      {
        _mapManager.uiManager.ShowDeckList();  
      }
      else
      {
        _mapManager.uiManager.CloseDeckList();
      }
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