using System;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
  [CreateAssetMenu(fileName = "NewDeck", menuName = "MyMenu/Decks/Deck")]
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