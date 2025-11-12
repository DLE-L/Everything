using System;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Map
{
  public class btnDeleteSelect : MonoBehaviour
  {
    [SerializeField] private DeleteCard_UI _deleteCardUI; 
    private void Awake()
    {
      _deleteCardUI ??= transform.GetComponentInParent<DeleteCard_UI>();
    }

    private void OnClick(PointerEventData obj)
    {
      _deleteCardUI.ShowDeleteCardList();
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