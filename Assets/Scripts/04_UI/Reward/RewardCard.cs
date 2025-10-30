using System;
using Core;
using Data.Collectible.Card;
using GamePlay.Battle;
using GamePlay.Reward;
using TMPro;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIs.Reward
{
  public class RewardCard : MonoBehaviour
  {
    public CardSO CardSo { get; private set; }
    private CardSprite _cardSprite;
    [SerializeField] private CardUI _cardUI;
    [SerializeField] private Image _selectImage;
    [SerializeField] private RewardManager _rewardManager;

    [SerializeField]private bool _isActive;

    private void Awake()
    {
      _cardUI.imgFrame ??= transform.GetChild(0).GetComponent<Image>();
      _cardUI.imgIcon ??= transform.GetChild(1).GetComponent<Image>();
      _cardUI.imgName ??= transform.GetChild(2).GetComponent<Image>();
      _cardUI.imgCost ??= transform.GetChild(3).GetComponent<Image>();
      _cardUI.txtName ??= transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
      _cardUI.txtDescription ??= transform.GetChild(4).GetComponent<TextMeshProUGUI>();

      _rewardManager ??= FindAnyObjectByType<RewardManager>();
      _selectImage ??=  transform.GetChild(5).GetComponent<Image>();
    }

    public void SetRewardCard(CardSO cardSo)
    {
      CardSo = cardSo;
      _cardSprite = GameSystem.Instance.Battle.AssetLoader.CardSprite;
      _cardUI.imgFrame.sprite = GetFrameSprite(CardSo.Type);
      // TODO Icon Sprite
      _cardUI.imgName.sprite = GetNameSprite(CardSo.Type);
      _cardUI.imgCost.sprite = GetCostSprite(CardSo.Cost);
      _cardUI.txtName.text = CardSo.Name;
      _cardUI.txtDescription.text = CardSo.Description;
    }

    private void Onclick(PointerEventData obj)
    {
      _isActive = !_isActive;
      var isSuccess = _rewardManager.UpdateRewardResult(this);
      if (isSuccess) _selectImage.enabled = _isActive;
      else _isActive = !_isActive;
    }

    private void OnEnable()
    {
      _isActive = false;
      _selectImage.enabled = false;
      UI_EventHandler.Get(gameObject).OnClickAction += Onclick;
    }

    private void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= Onclick;
    }


    #region Set Sprite
    private Sprite GetNameSprite(CardTypeSO type)
    {
      return type switch
      {
        CardTypeAttack => _cardSprite.Attack_Name,
        CardTypePower => _cardSprite.Power_Name,
        CardTypeSkill => _cardSprite.Skill_Name,
        _ => null
      };
    }
    private Sprite GetFrameSprite(CardTypeSO type)
    {
      return type switch
      {
        CardTypeAttack => _cardSprite.Attack_Frame,
        CardTypePower => _cardSprite.Power_Frame,
        CardTypeSkill => _cardSprite.Skill_Frame,
        _ => null
      };
    }
    private Sprite GetCostSprite(int cost)
    {
      return cost switch
      {
        0 => _cardSprite.Cost_0,
        1 => _cardSprite.Cost_1,
        2 => _cardSprite.Cost_2,
        3 => _cardSprite.Cost_3,
        4 => _cardSprite.Cost_4,
        5 => _cardSprite.Cost_5,
        6 => _cardSprite.Cost_6,
        7 => _cardSprite.Cost_7,
        8 => _cardSprite.Cost_8,
        9 => _cardSprite.Cost_9,
        _ => null
      };
    }
    #endregion
  }
}