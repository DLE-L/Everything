using System.Collections.Generic;
using Data.Collectible;
using Data.Collectible.Card;
using Data.Collectible.Relic;


namespace Data.Reward
{
  public class RewardData
  {
    public List<CardSO> CardsToPresent;
    public List<RelicSO> RelicsToPresent;
    public int Gold;
    
    public int SelectableCardCount;
    public int SelectableRelicCount;
  }
}