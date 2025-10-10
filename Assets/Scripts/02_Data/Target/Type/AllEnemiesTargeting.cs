using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using GamePlay.Units;

namespace Data.Target
{
  [CreateAssetMenu(fileName = "Target_AllEnemies",menuName = "MyMenu/Target/AllEnemies")]
  public class AllEnemiesTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(TargetingContext context)
    {
      return Task.FromResult(context.Enemies);
    }
  }
}