
using UnityEngine.EventSystems;

namespace GameSystem.Scene.Title
{
  public class btnContinueGame : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      GameSystem.Instance.LoadLobbyScene();
      // TODO: 이어서 게임 시작
      // 1. 플레이어 덱 리스트 로드
      // 2. 플레이어 덱 로드
      // 3. 플레이어 스탯 로드
    }
  }
}