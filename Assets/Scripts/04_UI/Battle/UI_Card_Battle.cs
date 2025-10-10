using Utils;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using GamePlay.Battle;
using Data.Card;
using UI.Common;

namespace UI.Battle
{
  public class UI_Card_Battle : MonoBehaviour
  {
    public event Action<BattleCardData> OnCardClicked;
    [SerializeField] private CardUI cardUI;

    public BattleCardData CardData;
    private BattleManager battleManager;

    private void Awake()
    {
      battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      cardUI = new()
      {
        imgCardFrame = transform.GetChild(0).GetComponent<Image>(),
        imgCardIcon = transform.GetChild(1).GetComponent<Image>(),
        imgName = transform.GetChild(2).GetComponent<Image>(),
        imgCost = transform.GetChild(3).GetComponent<Image>(),
        txtName = transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>(),
        txtDescription = transform.GetChild(4).GetComponent<TextMeshProUGUI>(),
      };
    }

    private void Start()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnCardClicked?.Invoke(this.CardData);
      };
    }

    public void SetUp(BattleCardData cardData)
    {
      CardData = cardData;


    }

    public void UpdateUI()
    {
      // switch (BattleCardData.CardSO.CardType) // TODO: 카드 종류에 맞는 Sprite추가
      // {
      //   case CardType.Attack:

      //     break;
      //   case CardType.Deffence:

      //     break;
      //   case CardType.Skill:

      //     break;
      //   default:
      //     break;
      // }

      //cardUI.UpdateUI(frame.sprite, icon.sprite, name.sprite, cost.sprite, BattleCardData.CardSO.CardName, BattleCardData.CardSO.Description);
    }

    public void SetUI()
    {
          // frame = ;
          // icon = ;
          // name = ;
          // cost = ;
    }
  }
}