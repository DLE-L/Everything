
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Game
{
  public class btnBattleEnter : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      GameSystem.Instance.LoadBattleScene();
      // TODO: 게임 시작
      // 1. 맵 생성
      // 2. 플레이어 배치
    }
  }
}