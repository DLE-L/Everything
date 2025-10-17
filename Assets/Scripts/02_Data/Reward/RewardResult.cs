using System.Collections.Generic;
using Data.Collectible.Card;
using Data.Collectible.Relic;

namespace Data.Reward
{
  public class RewardResult
  {
    public List<CardSO> Cards;
    public List<RelicSO> Relics;
    public int Gold;
  }
}