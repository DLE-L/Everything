using GamePlay.Units;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Data.Target
{
  [CreateAssetMenu(fileName = "Target_Self", menuName = "MyMenu/Target/Self")]
  public class SelfTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(TargetingContext context)
    {
      return Task.FromResult(new List<Unit>() { context.User });
    }
  }
}
