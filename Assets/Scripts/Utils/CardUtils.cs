using System;
using Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
  public enum CardType
  {
    Attack,
    Deffence,
    Skill,
  }

  public enum CardEffectType
  {
    DealDamage,
    GainBlock,
  }

  [Serializable]
  public class BattleCardData
  {
    public CardSO CardSO;
    public string BattleCardID;

    public BattleCardData(string cardObjectID, string cardId)
    {
      CardSO = CardDatabase.GetCardData(cardObjectID);
      BattleCardID = cardId;
    }
  }

  [Serializable]
  public class CardUI
  {
    public Image imgCardFrame;
    public Image imgCardIcon;
    public Image imgName;
    public Image imgCost;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtDescription;

    public CardUI() {}
    public void UpdateUI(Sprite frame, Sprite icon, Sprite name, Sprite cost, string nameText, string description)
    {
      imgCardFrame.sprite = frame;
      imgCardIcon.sprite = icon;
      imgName.sprite = name;
      imgCost.sprite = cost;
      txtName.text = nameText;
      txtDescription.text = description;
    }
  }
}
