using Units;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Item
{
  [CreateAssetMenu(fileName = "Targeting_AllEnemies",menuName = "MyMenu/Target/AllEnemies")]
  public class AllEnemiesTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(TargetingContext context)
    {
      return Task.FromResult(context.Enemies);
    }
  }
}