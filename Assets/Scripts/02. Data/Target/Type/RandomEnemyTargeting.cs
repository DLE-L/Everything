using GamePlay.Character;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Data.Target
{
  [CreateAssetMenu(fileName = "Targeting_RandomEnemy", menuName = "MyMenu/Target/RandomEnemy")]
  public class RandomEnemyTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(TargetingContext context)
    {
      System.Random rand = new System.Random();
      int randomIndex = rand.Next(0, context.Enemies.Count);
      return Task.FromResult(new List<Unit> { context.Enemies[randomIndex] });
    }
  }
}
