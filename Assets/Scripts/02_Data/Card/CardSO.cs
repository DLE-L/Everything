using System;
using System.Collections.Generic;
using Data.Effect;
using Data.Rarity;
using Data.Target;
using UnityEngine;

namespace Data.Card
{
  [Serializable]
  [CreateAssetMenu(fileName = "Card_", menuName = "MyMenu/Card/Card")]
  public class CardSO : ScriptableObject
  {
    public string Name;
    public Sprite Illustration;    
    public RaritySO Rarity;
    public int Cost;
    public string Description;
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