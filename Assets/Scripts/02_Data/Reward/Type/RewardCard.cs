using GamePlay.Units;
using UnityEngine;
using System.Threading.Tasks;

namespace Data.Reward.Type
{
  [CreateAssetMenu(fileName = "Reward_Card_", menuName = "MyMenu/Reward/Card")]
  public class RewardCard : RewardSO
  {
    public override Task GrantRewardAsync(Player player)
    {
      throw new System.NotImplementedException();
    }
  }
}