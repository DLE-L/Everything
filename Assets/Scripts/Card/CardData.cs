using System;
using System.Collections.Generic;

namespace Card.Data
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
  }

}
