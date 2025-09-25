using Units;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Item
{
  [CreateAssetMenu(fileName = "Targeting_RandomEnemy", menuName = "MyMenu/Target/RandomEnemy")]
  public class RandomEnemyTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(Unit user, List<Unit> allAllies, List<Unit> allEnemies)
    {
      System.Random rand = new System.Random();
      int randomIndex = rand.Next(0, allEnemies.Count);
      return Task.FromResult(new List<Unit> { allEnemies[randomIndex] });
    }
  }
}
