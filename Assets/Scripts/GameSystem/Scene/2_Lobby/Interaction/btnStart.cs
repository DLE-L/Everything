using UnityEngine;
using Utils;
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Lobby
{
  public class btnStart : MonoBehaviour
  {
    private void OnClick(PointerEventData data)
    {
      SystemEvent.RaiseSceneLoadStart("3_Game");
      SystemEvent.RaiseOnStartNewRun();
      // TODO: 게임 시작
      // 1. 맵 생성
      // 2. 플레이어 배치
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }
    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
    }

  }
}