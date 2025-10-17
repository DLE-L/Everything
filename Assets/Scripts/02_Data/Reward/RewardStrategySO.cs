using System.Threading.Tasks;
using UnityEngine;

namespace Data.Reward
{
  public abstract class RewardStrategySO : ScriptableObject
  {
    public abstract Task<RewardData> GenerateRewardAsync();
  }
}