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
  public class CardData
  {
    public bool IsDefaultCard;
    public CardType CardType;
    public string Name;
    public int Damage;
    public int Cost;
    public string Explain;

    public CardData(CardSO so)
    {
      IsDefaultCard = so.IsDefaultCard;
      CardType = so.CardType;
      Name = so.Name;
      Damage = so.Damage;
      Cost = so.Cost;
      Explain = so.Explain;
    }
  }

}
