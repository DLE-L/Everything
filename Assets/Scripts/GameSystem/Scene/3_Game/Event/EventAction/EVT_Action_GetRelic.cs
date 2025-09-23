using Units.Player;
using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class EVT_Action_GetRelic : IEventAction
  {
    public string Relic;
    public EVT_Action_GetRelic(string relic)
    {
      Relic = relic;
    }

    public void Execute(PlayerAction action)
    {
      action.AddRelic(Relic);
      Debug.Log($"유물 {Relic} 획득");
    }
  }
}