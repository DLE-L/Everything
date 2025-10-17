using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Collectible.Card;
using Data.Rarity;
using UnityEngine;

namespace Data.Reward.Type
{
  [CreateAssetMenu(fileName = "Reward_Random_", menuName = "MyMenu/Reward/RandomPool")]
  public class RewardStrategyRandomPool : RewardStrategySO
  {
    public int CardChoices;
    public int goldMin;
    public int goldMax;
    public int SelectableCardCount = 1;
    
    public List<RaritySO> Rarities;
    public override async Task<RewardData> GenerateRewardAsync()
    {
      System.Random random = new();
      var cardLoadingTasks = Rarities.Select(CardDatabase.GetCardsToRarityAsync);
      var results = await Task.WhenAll(cardLoadingTasks);
      
      var selectedCardChoices = results
        .SelectMany(cardList => cardList) // 여러 리스트를 하나로 병합
        .OrderBy(card => random.Next())   // 무작위로 섞음
        .Take(CardChoices)                // 원하는 개수만큼 가져옴
        .ToList();                        // 최종 리스트로 만듦
      
      var rewardData = new RewardData()
      {
        CardsToPresent = new(selectedCardChoices),
        Gold = random.Next(goldMin, goldMax),
        SelectableCardCount = this.SelectableCardCount,
      };

      return rewardData;
    }
  }
}