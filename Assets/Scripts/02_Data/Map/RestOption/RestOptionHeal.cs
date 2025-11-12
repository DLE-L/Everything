using Data.Units;
using UnityEngine;

namespace Data.Map.RestOption
{
  [CreateAssetMenu(fileName = "RestOption_Heal", menuName = "MyMenu/Map/RestOption/Heal")]
  public class RestOptionHeal : RestOptionSO
  {
    [Range(0, 1)] public float healPercentage = 0.3f;
    public override void Execute(PlayerRunData runData)
    {
      var healAmount = (int)(runData.Stat.MaxHP * healPercentage);
      runData.Stat.Heal(healAmount);
    }
  }
}