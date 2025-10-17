using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Collectible.Card;
using Data.Collectible.Relic;
using UnityEngine;

namespace Data.Reward.Type
{
  [CreateAssetMenu(fileName = "Reward_Defined_", menuName = "MyMenu/Reward/Defined")]
  public class RewardStrategyDefinedList : RewardStrategySO
  {
    public int goldMin;
    public int goldMax;
    public int SelectableCardCount = 1;
    public int SelectableRelicCount = 1;
    
    public List<CardSO> Cards;
    public List<RelicSO> Relics;
    
    public override Task<RewardData> GenerateRewardAsync()
    {
      System.Random random = new();
      var rewardData = new RewardData
      {
        CardsToPresent = new List<CardSO>(Cards),
        RelicsToPresent = new List<RelicSO>(Relics),
        Gold = random.Next(goldMin, goldMax),
        SelectableCardCount = this.SelectableCardCount,
        SelectableRelicCount = this.SelectableRelicCount
      };
      return Task.FromResult(rewardData);
    }
  }
}