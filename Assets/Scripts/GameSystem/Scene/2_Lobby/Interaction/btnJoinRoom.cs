using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace GameSystems.Scene.Lobby
{
  public class btnJoinRoom : InteractableBase
  {
    public TMP_InputField roomId;
    public override void OnPointerClick(PointerEventData eventData)
    {
      var network = FindAnyObjectByType<NetWorkPhoton>();
      network.JoinRoom(roomId.text);
    }
  }
}