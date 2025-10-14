using System;
using Data.Act.Encounter;
using GamePlay.Map;
using GamePlay.Units;
using UnityEngine.SceneManagement;
using Data.Reward;

namespace Core.Event
{
  public static class SystemEvent
  {
    #region Game Event
    public static event Action OnStartNewRun;
    public static void RaiseOnStartNewRun() => OnStartNewRun?.Invoke();
    public static event Action<Node> OnClickNode;
    public static void RaiseOnClickNode(Node node) => OnClickNode?.Invoke(node);
    #endregion

    #region Resource Event
    public static event Action<Unit, int> OnGainGold;
    public static void RaiseGainGold(Unit owner, int gold) => OnGainGold?.Invoke(owner, gold);
    public static event Action<Unit> OnEnterShop;
    public static void RaiseEnterShop(Unit owner) => OnEnterShop?.Invoke(owner);
    public static event Action<Unit> OnRest;
    public static void RaiseRest(Unit owner) => OnRest?.Invoke(owner);
    #endregion
  }
}