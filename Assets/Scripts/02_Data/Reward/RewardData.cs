using System.Collections.Generic;
using Data.Collectible;
using Data.Collectible.Card;
using Data.Collectible.Relic;


namespace Data.Reward
{
  public class RewardData
  {
    public List<CardSO> Cards;
    public List<RelicSO> Relics;
    public int Gold;

    public RewardData(List<CardSO> cards, List<RelicSO> relics, int gold)
    {
      Cards = new(cards);
      Relics = new(relics);
      Gold = gold;
    }
    
    public RewardData(RewardSO reward)
    {
      Cards = new(reward.Cards);
      Relics = new(reward.Relics);
      Gold = reward.Gold;
    }
  }
}