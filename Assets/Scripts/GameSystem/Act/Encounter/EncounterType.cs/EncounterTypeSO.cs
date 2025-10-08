using UnityEngine;

namespace GameSystems.Act
{  
  public abstract class EncounterTypeSO : ScriptableObject
  {
    public string Name;
    public Sprite Icon;
    public string Description;
    public abstract void BeginEncounter();
  }
}

/*
  public enum EncounterType
  {
    Narrative,
    Combat,    
    Shop,
    Rest,
    Boss,
  }
*/