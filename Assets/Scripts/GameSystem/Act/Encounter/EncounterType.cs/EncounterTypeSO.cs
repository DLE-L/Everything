
using System;
using UnityEngine;

namespace GameSystems.Act
{
  [Serializable]
  public abstract class EncounterTypeSO : ScriptableObject
  {
    public string Name;
    public Sprite Icon;
    public Color Color;
    public string Description;
  }
}

/*
  public enum EncounterType
  {
    Narrative,
    Combat,
    EliteCombat,    
    Shop,
    Rest,
    Boss,
  }
*/