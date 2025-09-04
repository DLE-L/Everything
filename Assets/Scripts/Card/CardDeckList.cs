using System;
using System.Collections.Generic;
using Utils;


namespace GameSystem
{
  [Serializable]
  public class CardDeckList
  {
    public Dictionary<CardDeckData, string> cardDeckList = new(); // Dictionary<CardDeckData ,CardDeckId>

    public void Init(CardDeckData deckData)
    {
      string deckId = Guid.NewGuid().ToString();
      cardDeckList.Add(deckData, deckId);
    }
  }
}