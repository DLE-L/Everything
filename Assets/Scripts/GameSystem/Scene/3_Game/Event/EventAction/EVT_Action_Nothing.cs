using Units.Player;
using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class EVT_Action_Nothing : IEventAction
  {
    public void Execute(PlayerAction action)
    {
      Debug.Log("[Event_UI창 종료]");
    }
  }
}