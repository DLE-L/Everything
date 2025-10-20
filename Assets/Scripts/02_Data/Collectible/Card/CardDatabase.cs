using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Rarity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Data.Collectible.Card
{
   public static class CardDatabase
  {
    public static Dictionary<string, CardSO> AllCards { get; private set; } = new(); // Dictionary<CardId, CardSO>
    private static readonly HashSet<string> _defaultCardIDs = new()
    {
      "Card_Attack_Basic_Strike",
      "Card_Skill_Basic_Defend",
    };

    public static async Task InitializeAsync()
    {
      var cardList = await AssetLoader.LoadAssetsByLabelAsync<CardSO>("Card");
      foreach (var card in cardList)
      {        
        AllCards.TryAdd(card.name, card);
      }
      Debug.Log($"CardDatabase Initialized");
    }

    public static async Task<IList<CardSO>> GetCardsToRarityAsync(RaritySO rarity)
    {
      List<string> rarityLabel = new() { "Card", rarity.Name };
      var cardList = await AssetLoader.LoadAssetsByLabelsAsync<CardSO>(rarityLabel);
      return cardList;
    }
  }

  [Serializable]
  public class CardUI
  {
    public Image imgFrame;
    public Image imgIcon;
    public Image imgName;
    public Image imgCost;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtDescription;

    public CardUI() {}
    public void UpdateUI(Sprite frame, Sprite icon, Sprite name, Sprite cost, string nameText, string description)
    {
      imgFrame.sprite = frame;
      imgIcon.sprite = icon;
      imgName.sprite = name;
      imgCost.sprite = cost;
      txtName.text = nameText;
      txtDescription.text = description;
    }
  }
}
