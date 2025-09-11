
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Title
{
  public class btnContinueGame : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      GameSystem gameSystem = GameSystem.Instance;
      gameSystem.LoadLobbyScene();
      gameSystem.ContinueGameStart();
    }
  }
}