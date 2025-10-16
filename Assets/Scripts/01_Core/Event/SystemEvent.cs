using System;
using GamePlay.Map;
using GamePlay.Units;
using Data.Reward;
using Data.Units;
using UIs.Reward;

namespace Core.Event
{
  public static class SystemEvent
  {
    #region Game Event
    public static event Action<PlayerRunData> OnBeforeStartNewRun;
    public static void RaiseBeforeStartNewRun(PlayerRunData data) => OnBeforeStartNewRun?.Invoke(data);
    public static event Action OnStartNewRun;
    public static void RaiseStartNewRun() => OnStartNewRun?.Invoke();
    public static event Action OnEndRun;
    public static void RaiseEndRun() => OnEndRun?.Invoke();
    public static event Action<Node> OnClickNode;
    public static void RaiseClickNode(Node node) => OnClickNode?.Invoke(node);
    public static event Action<RewardData> OnGrantsReward;
    public static void RaiseGrantsReward(RewardData reward) => OnGrantsReward?.Invoke(reward);
    #endregion

    #region Resource Event
    public static event Action<Unit, int> OnGainGold;
    public static void RaiseGainGold(Unit owner, int gold) => OnGainGold?.Invoke(owner, gold);
    public static event Action<Unit> OnEnterShop;
    public static void RaiseEnterShop(Unit owner) => OnEnterShop?.Invoke(owner);
    public static event Action<Unit> OnEnterRest;
    public static void RaiseEnterRest(Unit owner) => OnEnterRest?.Invoke(owner);
    #endregion
  }
}