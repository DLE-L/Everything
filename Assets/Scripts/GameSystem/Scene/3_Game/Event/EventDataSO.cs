using UnityEngine;
using System.Collections.Generic;
using Units.Player;
using System;
using Item;

namespace GameSystems.Scene.Game
{
  [CreateAssetMenu(fileName = "EventData", menuName = "MyMenu/EventData")]
  public class EventDataSO : ScriptableObject
  {
    public string Name;
    public string Description;
    public List<ChoiceButton> ChoiceList;
  }

  public class EventData
  {
    public string Name;
    public string Description;
    public List<ChoiceButton> ChoiceList;

    public EventData(EventDataSO dataSO)
    {
      Name = dataSO.Name;
      Description = dataSO.Description;
      ChoiceList = new(dataSO.ChoiceList);
    }    
  }

  public interface IEventAction
  {
    public void Execute(PlayerAction action);
  }

  [Serializable]
  public class ChoiceButton
  {
    public string ChoiceText;
    public List<EventResult> EventResult;
    private Dictionary<EventActionType, IEventAction> _actions;  
  }

  [Serializable]
  public class EventResult
  {
    public EventActionType ActionType;
    public int Value;
    public RelicSO Relic;
  }

  public enum EventActionType
  {
    GetRelic,
    UpdateHP,
    Nothing,
  }
}
