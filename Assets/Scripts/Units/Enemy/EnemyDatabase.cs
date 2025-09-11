
using System.Collections.Generic;
using Utils;

namespace Units.Enemy
{
  public static class EnemyDatabase
  {
    public static Dictionary<string, EnemySO> enemyDatabase = new(); // Dictionary<EnmeyId, EnemyData>      

    public static void Init()
    {
      LoadEnemyData();
    }

    public async static void LoadEnemyData()
    {
      var enemyList = await AssetLoader.LoadAssetLabelAsync<EnemySO>("Enemy");
      foreach (var enemy in enemyList)
      {        
        enemyDatabase.Add(enemy.EnemyId, enemy);
      }
    }

    public static EnemySO GetEnemyData(string enemyId)
    {
      if (enemyDatabase.ContainsKey(enemyId))
      {
        return enemyDatabase[enemyId];
      }
      return null;
    }

  }
}