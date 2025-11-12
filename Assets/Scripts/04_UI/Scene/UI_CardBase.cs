using Core;
using Data.Collectible.Card;
using GamePlay.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
  public abstract class UI_CardBase : MonoBehaviour
  {
    private CardSprite _cardSprite;
    [SerializeField] protected CardUI _cardUI;

    private void Awake()
    {
      _cardUI.imgFrame ??= transform.GetChild(0).GetComponent<Image>();
      _cardUI.imgIcon ??=  transform.GetChild(1).GetComponent<Image>();
      _cardUI.imgName ??= transform.GetChild(2).GetComponent<Image>();
      _cardUI.imgCost ??=  transform.GetChild(3).GetComponent<Image>();
      _cardUI.txtName ??= transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
      _cardUI.txtDescription ??= transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    protected void SetupCard_UI(RuntimeCard card)
    {
      _cardSprite = GameSystem.Instance.Map.AssetLoader.CardSprite;
      // ... UI 업데이트 ...
      _cardUI.imgFrame.sprite = GetFrameSprite(card.Data.Type);
      // TODO Icon Sprite
      _cardUI.imgName.sprite = GetNameSprite(card.Data.Type);
      _cardUI.imgCost.sprite = GetCostSprite(card.Data.Cost);
      _cardUI.txtName.text = card.Data.Name;
      _cardUI.txtDescription.text = card.Data.Description;
    }

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
  }
}