using System;
using GamePlay.Map;
using GamePlay.Character;
using UnityEngine.SceneManagement;
using Data.Act;

namespace Core.Event
{
  public static class SystemEvent
  {
    #region GameSystemEvent
    public static event Action OnGameSystemInit;
    public static void RaiseGameSystemInit() => OnGameSystemInit?.Invoke();
    public static event Action OnGameSystemExit;
    public static void RaiseGameSystemExit() => OnGameSystemExit?.Invoke();
    #endregion

    #region Game Event
    public static event Action OnClickNewGame;
    public static void RaiseOnClickNewGame() => OnClickNewGame?.Invoke();
    public static event Action OnClickContinueGame;
    public static void RaiseOnClickContinueGame() => OnClickContinueGame?.Invoke();
    public static event Action OnStartNewRun;
    public static void RaiseOnStartNewRun() => OnStartNewRun?.Invoke();
    public static event Action<Node> OnClickNode;
    public static void RaiseOnClickNode(Node node) => OnClickNode?.Invoke(node);
    public static event Action<RewardSO> OnChoiceReward;
    public static void RaiseOnChoiceReward(RewardSO reward) => OnChoiceReward?.Invoke(reward);
    #endregion

    #region Data Event
    public static event Action OnSaveDataStarted;
    public static void RaiseSaveDataStarted() => OnSaveDataStarted?.Invoke();
    public static event Action<bool> OnSaveDataCompleted;
    public static void RaiseSaveDataCompleted(bool success) => OnSaveDataCompleted?.Invoke(success);
    public static event Action OnLoadDataStarted;
    public static void RaiseLoadDataStarted() => OnLoadDataStarted?.Invoke();
    public static event Action<bool> OnLoadDataCompleted;
    public static void RaiseLoadDataCompleted(bool success) => OnLoadDataCompleted?.Invoke(success);
    #endregion

    #region Scene Event
    public enum GameState { MainMenu, Lobby, InGame, Paused }
    public static event Action<GameState> OnGameStateChanged;
    public static void RaiseGameStateChanged(GameState newState) => OnGameStateChanged?.Invoke(newState);

    public static event Action<string, LoadSceneMode> OnSceneLoadStart;
    public static void RaiseSceneLoadStart(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) => OnSceneLoadStart?.Invoke(sceneName, mode);
    public static event Action<string> OnSceneLoadEnd;
    public static void RaiseSceneLoadEnd(string sceneName) => OnSceneLoadEnd?.Invoke(sceneName);
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