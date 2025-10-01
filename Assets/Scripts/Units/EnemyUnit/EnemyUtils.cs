using Item;
using Utils;
using System.Collections.Generic;

namespace Units
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