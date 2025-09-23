using UnityEngine;
using Utils;
using System;

namespace GameSystems.Scene.Lobby
{
  public class btnStart : MonoBehaviour
  {
    public event Action OnClickStartGame;

    public void OnClickStart()
    {
      GameSystem.Instance.LoadGameScene();
      // TODO: 게임 시작
      // 1. 맵 생성
      // 2. 플레이어 배치
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnClickStartGame?.Invoke();
      };

      OnClickStartGame += OnClickStart;
    }
    void OnDisable()
    {
      OnClickStartGame -= OnClickStart;
    }

  }
}