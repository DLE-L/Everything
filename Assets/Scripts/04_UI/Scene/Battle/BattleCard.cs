using System;
using Core;
using UnityEngine;
using TMPro;
using Data.Collectible.Card;
using GamePlay.Battle;
using UnityEngine.UI;
using Utils;

namespace UIs.Battle
{
  [RequireComponent(typeof(DragCard))]
  public class BattleCard : UI_CardBase, IPoolableObject
  {
    private DragCard _dragCard;
    public RuntimeCard RuntimeCard { get; private set; }

    public void Setup(RuntimeCard card)
    {
      SetupCard_UI(card);
      RuntimeCard = card;
    }
    
    public void ResetState()
    {
      _cardUI.imgFrame.sprite = null;
      // TODO Icon Sprite
      _cardUI.imgName.sprite = null;
      _cardUI.imgCost.sprite = null;
      _cardUI.txtName.text = null;
      _cardUI.txtDescription.text = null;
    }
  }
}