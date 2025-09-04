using System;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;

namespace Utils
{
  public enum CardType
  {
    Attack,
    Deffence,
    Skill,
  }  

  [Serializable]
  public class CardDeckData
  {
    public List<string> Cards = new();
    
  }

  [Serializable]
  public class CardData
  {
    public CardType CardType;
    public string Name;
    public int Damage;
    public int Cost;
    public string Explain;

    public CardData(CardScriptableObject so)
    {
      CardType = so.CardType;
      Name = so.Name;
      Damage = so.Damage;
      Cost = so.Cost;
      Explain = so.Explain;
    }
  }

}
