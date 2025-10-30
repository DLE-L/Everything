using GamePlay.Units;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Target
{
  [CreateAssetMenu(fileName = "Target_SingleEnemy" ,menuName = "MyMenu/Target/SingleEnemy")]
  public class SingleEnemyTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(TargetingContext context)
    {
      var targets = new List<Unit>();
      if (context.User is EnemyController)
      {
        System.Random random = new();
        targets = new List<Unit>() { context.Enemies[random.Next(0, context.Enemies.Count)]};
      }
      else if (context.User is Player)
      {
        targets = new List<Unit>() { context.TargetUnit };
      }
      
      return Task.FromResult(targets);
    }
  }
}