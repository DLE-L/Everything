using Units;
using System.Collections.Generic;
using UnityEngine;
using GameSystems;
using System.Threading.Tasks;

namespace Item
{
  [CreateAssetMenu(fileName = "Targeting_Self", menuName = "MyMenu/Target/Self")]
  public class SelfTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(Unit user, List<Unit> allAllies, List<Unit> allEnemies)
    {
      return Task.FromResult(new List<Unit>() { user });
    }
  }
}
