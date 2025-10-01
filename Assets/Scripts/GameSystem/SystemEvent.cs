using System;
using Units;
using UnityEngine.SceneManagement;

namespace GameSystems
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
    public static event Action OnClickNewRun;
    public static void RaiseOnClickStartNewRun() => OnClickNewRun?.Invoke();
    public static event Action OnStartNewRun;
    public static void RaiseOnStartNewRun() => OnStartNewRun?.Invoke();
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
  }
}