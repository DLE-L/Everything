using Core;
using Data.Units;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Map
{
  public class btnDelete : MonoBehaviour
  {
    [SerializeField] private DeleteCard_UI _deleteCardUI;

    private void Awake()
    {
      _deleteCardUI ??= transform.GetComponentInParent<DeleteCard_UI>();
    }

    private void OnClick(PointerEventData obj)
    {
      var deleteCard = _deleteCardUI.DeleteCard.Card;
      if (deleteCard is null) return;
      PlayerRunAction.RemoveCardFromDeckPermanently(deleteCard);
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