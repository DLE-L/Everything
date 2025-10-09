using UnityEngine;

namespace Data.Map
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