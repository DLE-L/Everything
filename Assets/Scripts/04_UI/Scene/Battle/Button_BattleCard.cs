using System;
using UnityEngine;
using TMPro;
using Data.Collectible.Card;
using UIs.Common;
using UnityEngine.EventSystems;
using Core.Event;
using UnityEngine.UI;

namespace UIs.Battle
{
  public class Button_BattleCard : MonoBehaviour
  {
    [SerializeField] private CardUI _cardUI;
    public CardSO CardSo { get; private set; }

    private void Awake()
    {
      _cardUI.imgFrame ??= transform.Find("imgFrame").GetComponent<Image>();
      _cardUI.imgIcon ??=  transform.Find("imgIcon").GetComponent<Image>();
      _cardUI.imgName ??= transform.Find("imgName").GetComponent<Image>();
      _cardUI.imgCost ??=  transform.Find("imgCost").GetComponent<Image>();
      _cardUI.txtName ??= transform.Find("txtName").GetComponent<TextMeshProUGUI>();
      _cardUI.txtDescription ??=  transform.Find("txtDescription").GetComponent<TextMeshProUGUI>();
    }

    public void Setup(CardSO card)
    {
      CardSo = card;
      
      // ... UI 업데이트 ...
    }
    
    public void OnClick(PointerEventData data)
    {
      BattleEvent.RaiseBattleCardClick(this);
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