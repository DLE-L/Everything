using Units;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Item
{
  [CreateAssetMenu(fileName = "Targeting_SingleEnemy" ,menuName = "MyMenu/Target/SingleEnemy")]
  public class SingleEnemyTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(TargetingContext context)
    {
      return Task.FromResult(new List<Unit>() { user });
    }
  }
}