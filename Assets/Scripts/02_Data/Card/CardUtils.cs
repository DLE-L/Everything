using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Data.Card
{
   public static class CardDatabase
  {
    public static Dictionary<string, CardSO> AllCards { get; private set; } = new(); // Dictionary<CardId, CardSO>      
    private static readonly HashSet<string> _defaultCardIDs = new()
    {
      "Card_Attack_Common_Strike",
      "Card_Skill_Common_Defend",
      "Card_Attack_Common_Bash",
      "Card_Skill_Uncommon_Uplift"
    };

    public static async Task InitializeAsync()
    {
      var cardList = await AssetLoader.LoadAssetLabelAsync<CardSO>("Card");
      foreach (var card in cardList)
      {        
        AllCards.TryAdd(card.name, card);
      }
      Debug.Log($"[CardDatabase] Initialized");
    }

    public static bool IsDefaultCard(string cardID)
    {
      return _defaultCardIDs.Contains(cardID);
    }
  }

  [Serializable]
  public class BattleCardData
  {
    public CardSO CardSO;
    public string BattleCardID;

    public BattleCardData(string cardObjectID, string cardId)
    {
      CardSO = CardDatabase.AllCards[cardObjectID];
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
