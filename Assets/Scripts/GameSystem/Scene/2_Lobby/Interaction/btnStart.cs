using UnityEngine;
using Utils;
using System;
using UnityEngine.EventSystems;
using Units;

namespace GameSystems.Scene.Lobby
{
  public class btnStart : MonoBehaviour
  {
    public event Action OnClickStartGame;

    public void OnClickStart(PointerEventData data)
    {
      SystemEvent.RaiseSceneLoadStart("3_Game");
      SystemEvent.RaiseOnClickStartNewRun();
      // TODO: 게임 시작
      // 1. 맵 생성
      // 2. 플레이어 배치
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClickStart;
    }
    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClickStart;
    }

  }
}