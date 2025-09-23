
using UnityEngine;
using Utils;
using System;

namespace GameSystems.Scene.Title
{
  public class btnContinueGame : MonoBehaviour
  {
    public event Action OnClickContinueGame;

    public void OnClickContinue()
    {
      GameSystem gameSystem = GameSystem.Instance;
      gameSystem.LoadLobbyScene();
    }
    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
            {
              OnClickContinueGame?.Invoke();
            };

      OnClickContinueGame += OnClickContinue;
    }
    void OnDisable()
    {
      OnClickContinueGame -= OnClickContinue;
    }
  }
}
