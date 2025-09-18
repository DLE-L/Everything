using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  [CreateAssetMenu(fileName = "Event", menuName = "MyMenu/Event")]
  public class EventSO : ScriptableObject
  {
    public string Name;
    public string Description;
    public List<EventChoice> Choices = new();
  }
}