
using UnityEngine;

namespace GameSystems.Act
{
  [CreateAssetMenu(fileName = "NarrativeType", menuName = "MyMenu/Act/EncounterType/Narrative")]
  public class NarrativeEncounterType : EncounterTypeSO
  {
    public override void BeginEncounter()
    {
      //GameSystem.Instance.Game.event_UI.enabled = true;
      Debug.Log($"이벤트 호출합니다");
    }
  }
}