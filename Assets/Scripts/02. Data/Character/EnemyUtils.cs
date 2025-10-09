using Utils;
using System.Collections.Generic;
using Data.Card;

namespace Data.Character
{
  public class BattleEnemyData
  {    
    public StatData Stat;
    public List<CardSO> AbilityCards;

    public BattleEnemyData(EnemySO enemySO)
    {      
      // Stat = new StatData(enemySO.Stat);
      // AbilityCards = new(enemySO.AbilityCards);
    }
  }
}