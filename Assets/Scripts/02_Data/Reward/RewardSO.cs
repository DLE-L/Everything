using System;
using System.Collections.Generic;
using Data.Collectible;
using Data.Collectible.Card;
using Data.Collectible.Relic;
using UnityEngine;

namespace Data.Reward
{
  [CreateAssetMenu(fileName = "Reward_",  menuName = "MyMenu/Reward/Reward")]
  public class RewardSO : ScriptableObject
  {
    [Header("Reward")]
    public List<CardSO> Cards;
    public List<RelicSO> Relics;
    public int Gold;
    [Header("Able Selection")]
    public int SelectAbleCardCount;
    public int SelectAbleRelicCount;
  }
}