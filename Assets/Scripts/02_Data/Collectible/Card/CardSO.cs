using System;
using System.Collections.Generic;
using Data.Collectible;
using Data.Effect;
using Data.Rarity;
using Data.Target;
using UnityEngine;

namespace Data.Collectible.Card
{
  [Serializable]
  [CreateAssetMenu(fileName = "Card_", menuName = "MyMenu/Card/Card")]
  public class CardSO : CollectibleSO
  {
    public RaritySO Rarity;
    public int Cost;
    public bool Retain;
    public bool Exhaust;
    public CardTypeSO Type;
    public List<CardEffect> Effects;
  }

  [Serializable]
  public class CardEffect
  {
    public GameEffectSO Effect;
    public TargetingStrategySO Target;
  }
}