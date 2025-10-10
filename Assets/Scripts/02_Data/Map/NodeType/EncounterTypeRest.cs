using UnityEngine;

namespace Data.Map
{
  [CreateAssetMenu(fileName = "Encounter_RestType", menuName = "MyMenu/Map/EncounterType/Rest")]
  public class EncounterTypeRest : EncounterTypeSO
  {
    public override void BeginEncounter()
    {
      throw new System.NotImplementedException();
    }
  }
}