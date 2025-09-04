
using UnityEngine.EventSystems;

namespace GameSystem.Scene.Lobby
{
  public class btnStart : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      GameSystem.Instance.LoadGameScene();
      // TODO: 게임 시작
      // 1. 맵 생성
      // 2. 플레이어 배치
    }
  }
}