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
      return Task.FromResult(new List<Unit>() {  }); //TODO : 추가해야됨
    }
  }
}