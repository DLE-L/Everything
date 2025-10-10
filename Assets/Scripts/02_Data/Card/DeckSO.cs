using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Card
{
  [CreateAssetMenu(fileName = "Deck_", menuName = "MyMenu/Card/Deck")]
  public class DeckSO : ScriptableObject
  {
    public List<CardCount> Cards = new();    
  }

  [Serializable]
  public class CardCount
  {
    public CardSO Card;
    public int Count;
  }
}