using UnityEngine;
using System.Collections.Generic;
using Data.Card;
using Data.Relic;

namespace Data.Act
{
  public class RewardSO : ScriptableObject
  {
    public List<CardSO> Cards;
    public List<RelicSO> Relics;
  }
}