using GamePlay.Units;
using UnityEngine;
using System.Threading.Tasks;
using GamePlay.Battle;

namespace Data.Reward.Type
{
  [CreateAssetMenu(fileName = "Reward_Card_", menuName = "MyMenu/Reward/Card")]
  public class RewardCard : RewardSO
  {
    public override async Task GrantRewardAsync(Unit user, BattleManager manager)
    {
      throw new System.NotImplementedException();
    }
  }
}