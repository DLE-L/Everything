using Card;
using Units.Enemy;
using System.Collections.Generic;

namespace Utils
{
  public class BattleEnemyData
  {
    public string EnemyId;
    public StatData Stat;
    public List<CardSO> AbilityCards;

    public BattleEnemyData(EnemySO enemySO)
    {
      EnemyId = enemySO.EnemyId;
      Stat = new StatData(enemySO.Stat);
      AbilityCards = new(enemySO.AbilityCards);
    }
  }
}