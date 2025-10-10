using GamePlay.Units;
using UnityEngine;
using System.Threading.Tasks;

namespace Data.Reward
{
  public abstract class RewardSO : ScriptableObject
  {
    public string Description;
    public int Gold;
    public abstract Task GrantRewardAsync(Player player);
  }
}