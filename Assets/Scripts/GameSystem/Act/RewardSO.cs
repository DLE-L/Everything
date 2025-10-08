using Item;
using UnityEngine;
using System.Collections.Generic;

namespace GameSystems.Act
{
  public class RewardSO : ScriptableObject
  {
    public List<CardSO> Cards;
    public List<RelicSO> Relics;
  }
}