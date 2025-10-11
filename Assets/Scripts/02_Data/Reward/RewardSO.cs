using GamePlay.Units;
using UnityEngine;
using System.Threading.Tasks;
using GamePlay.Battle;

namespace Data.Reward
{
  public abstract class RewardSO : ScriptableObject
  {
    public string Description;
    public int Gold;
    public abstract Task GrantRewardAsync(Unit user, BattleManager manager);
  }
}