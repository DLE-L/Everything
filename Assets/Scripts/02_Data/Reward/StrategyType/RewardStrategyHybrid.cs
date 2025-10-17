using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Data.Reward.Type
{
  [CreateAssetMenu(fileName = "Reward_Hybrid_", menuName = "MyMenu/Reward/Hybrid")]
  public class RewardStrategyHybrid : RewardStrategySO
  {
    public int goldMin;
    public int goldMax;
    public List<RewardStrategySO> Strategies = new();

    public override async Task<RewardData> GenerateRewardAsync()
    {
      System.Random random = new();
      var rewardLoadingTasks = Strategies.Select(strategy => strategy.GenerateRewardAsync());
      var results = await Task.WhenAll(rewardLoadingTasks);

      var finalRewardData = new RewardData();
      foreach (var rewardData in results)
      {
        finalRewardData.CardsToPresent.AddRange(rewardData.CardsToPresent);
        finalRewardData.RelicsToPresent.AddRange(rewardData.RelicsToPresent);
        finalRewardData.SelectableCardCount = rewardData.SelectableCardCount;
        finalRewardData.SelectableRelicCount = rewardData.SelectableRelicCount;
        finalRewardData.Gold = random.Next(goldMin, goldMax);
      }

      return finalRewardData;
    }
  }
}