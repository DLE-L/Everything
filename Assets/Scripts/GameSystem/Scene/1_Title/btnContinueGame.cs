
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Title
{
  public class btnContinueGame : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      GameSystem.Instance.LoadLobbyScene();
      // TODO: 이어서 게임 시작      
      // 1. 플레이어 덱 로드
      // 2. 플레이어 스탯 로드
    }
  }
}