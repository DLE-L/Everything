using Units.Player;
using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class EVT_Action_UpdateHP : IEventAction
  {
    public int Amount;
    public EVT_Action_UpdateHP(int amount)
    {
      Amount = amount;
    }

    public void Execute(PlayerAction action)
    {      
      action.UpdateHealth(Amount);
      Debug.Log($"[HP {Amount} 업데이트][{GameSystem.Instance.Player.RunData.HP}]");
    }
  }
}