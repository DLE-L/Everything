using UnityEngine;

namespace Data.Map
{
  [CreateAssetMenu(fileName = "Encounter_NarrativeType", menuName = "MyMenu/Map/EncounterType/Narrative")]
  public class EncounterTypeNarrative : EncounterTypeSO
  {
    public override void BeginEncounter()
    {
      //GameSystem.Instance.Game.event_UI.enabled = true;
      Debug.Log($"이벤트 호출합니다");
    }
  }
}