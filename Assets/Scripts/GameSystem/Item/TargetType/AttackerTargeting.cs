using Units;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Item
{
  [CreateAssetMenu(fileName = "Targeting_Attacker", menuName = "MyMenu/Target/Attacker")]
  public class AttackerTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(Unit user, List<Unit> allAllies, List<Unit> allEnemies)
    {
      return Task.FromResult(new List<Unit>() { user });
    }
  }
}
