using Units.Enemy;

namespace Utils
{
  public class BattleEnemyData
  {
    public EnemySO Data;
    public string BattleEnemyID;

    public BattleEnemyData(string enemyObjectID, string enemyId)
    {
      Data = EnemyDatabase.GetEnemyData(enemyObjectID);
      BattleEnemyID = enemyId;
    }
  }
}