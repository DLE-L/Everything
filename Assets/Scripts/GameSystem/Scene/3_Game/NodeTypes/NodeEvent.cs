using System;
using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class NodeEvent : NodeScript
  {

    public override void Init()
    {

      var @event = EventDatabase.Events["EVT_AncientAltar"];
      Name = @event.Name;
      Description = @event.Description;
      ChoiceList = new(@event.ChoiceList);
      Debug.Log($"ChoiceList.Count: {@event.ChoiceList.Count}");
    }
  }
}