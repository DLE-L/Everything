using System;
using System.Collections.Generic;
using GamePlay.Battle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UIs.Units
{
  public class TargetHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    [SerializeField] private Image _targetImage;
    private BattleManager _battleManager;
    private static readonly List<TargetHighlight> _activeTargets = new();

    private void Awake()
    {
      _battleManager ??= FindAnyObjectByType<BattleManager>();
      SetHighlight(false);
    }

    private void Start()
    {
      if (_targetImage is null) Debug.LogWarning("Target Image is null");
      _activeTargets.Add(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (!_battleManager.IsDraggingCard()) return;
      
      SetHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      SetHighlight(false);
    }

    public static void ClearAllHighlights()
    {
      foreach (var target in _activeTargets)
      {
        target.SetHighlight(false);
      }
    }
    
    private void SetHighlight(bool active)
    {
      if(_targetImage is null) return;
      _targetImage.enabled = active;
    }

    private void OnDestroy()
    {
      _activeTargets.Remove(this);
    }
  }
}