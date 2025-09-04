
using System;
using System.Collections.Generic;
using GameSystem;
using Utils;
using UnityEngine;

namespace Card
{
  public class CardDatabase : MonoBehaviour
  {
    public HashSet<CardData> cardDatabase = new();
    public List<CardData> cards = new();

    void Start()
    {
      Init();
    }

    public void Init()
    {
      LoadCardData();
    }

    public async void LoadCardData()
    {
      var cardList = await AssetLoader.LoadAssetLabelAsync<CardScriptableObject>("Card");
      foreach (var card in cardList)
      {
        CardData data = new(card);
        cardDatabase.Add(data);
        cards.Add(data);
        Debug.Log(data.CardType);
      }
    }
  }
}