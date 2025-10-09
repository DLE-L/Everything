using GamePlay.Character;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Data.Target
{
  [CreateAssetMenu(fileName = "Targeting_Attacker", menuName = "MyMenu/Target/Attacker")]
  public class AttackerTargeting : TargetingStrategySO
  {
    public override Task<List<Unit>> FindTargetsAsync(TargetingContext context)
    {
      return Task.FromResult(new List<Unit>() { context.Attacker });
    }
  }
}
