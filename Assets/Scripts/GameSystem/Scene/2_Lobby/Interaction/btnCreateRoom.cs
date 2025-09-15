
using UnityEngine.EventSystems;
using Utils;

namespace GameSystems.Scene.Lobby
{
  public class btnCreateRoom : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      var network = FindAnyObjectByType<NetWorkPhoton>();
      network.CreateRoom();
    }
  }
}