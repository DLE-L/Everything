using System;
using Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Item
{
   public static class CardDatabase
  {
    public static Dictionary<string, CardSO> cardDatabase = new(); // Dictionary<CardId, CardData>      
    private static HashSet<string> _defaultCardIDs = new()
    {
      "Attack_Strike",
      "Deffence_Defend",
      "Attack_Bash",
      "Skill_Survivor"
    };

    public async static Task LoadCardData()
    {
      var cardList = await AssetLoader.LoadAssetLabelAsync<CardSO>("Card");
      foreach (var card in cardList)
      {
        cardDatabase.TryAdd(card.CardId, card);
      }
      return;
    }

    public static CardSO GetCardData(string cardId)
    {
      if (cardDatabase.ContainsKey(cardId))
      {
        return cardDatabase[cardId];
      }
      return null;
    }

    public static bool IsDefaultCard(string cardId)
    {
      return _defaultCardIDs.Contains(cardId);
    }
  }
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
