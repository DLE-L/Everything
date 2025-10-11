using GamePlay.Units;
using UnityEngine;
using System.Threading.Tasks;
using GamePlay.Battle;

namespace Data.Reward.Type
{
  [CreateAssetMenu(fileName = "Reward_Relic_", menuName = "MyMenu/Reward/Relic")]
  public class RewardRelic : RewardSO
  {
    public override async Task GrantRewardAsync(Unit user, BattleManager manager)
    {
      throw new System.NotImplementedException();
    }
  }
}