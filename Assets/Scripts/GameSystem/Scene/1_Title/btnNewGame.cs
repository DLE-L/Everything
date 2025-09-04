
using UnityEngine.EventSystems;

namespace GameSystem.Scene.Title
{
  public class btnNewGame : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      GameSystem.Instance.LoadLobbyScene();
      // TODO: 새로운 게임 시작
    }
  }
}