using GamePlay.Units;
using UnityEngine;
using System.Threading.Tasks;

namespace Data.Reward.Type
{
  [CreateAssetMenu(fileName = "Reward_Relic_", menuName = "MyMenu/Reward/Relic")]
  public class RewardRelic : RewardSO
  {
    public override Task GrantRewardAsync(Player player)
    {
      throw new System.NotImplementedException();
    }
  }
}