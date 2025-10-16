using System;
using Core;
using Core.Event;
using Data.Collectible;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Reward
{
  public class RewardItem : MonoBehaviour
  {
    private CollectibleSO _collectible;
    private Canvas_Reward_Combat _combat;
    
    private bool _isSelected;

    public void Init(CollectibleSO collectible, Canvas_Reward_Combat combat)
    {
      _collectible = collectible;
      _combat = combat;
    }

    private void OnClick(PointerEventData obj)
    {
      _isSelected = !_isSelected;
      bool success = _combat.SelectionItem(_collectible, _isSelected);
      if (!success && _isSelected)
      {
        Debug.Log($"AllReady selected");
        _isSelected = false;
      }
      Debug.Log($"[{_collectible.name}] selected");
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