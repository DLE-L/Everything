using System.Threading.Tasks;
using GamePlay.Map;
using UnityEngine;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Narrative_", menuName = "MyMenu/Act/Encounter/Narrative")]
  public class EncounterNarrative : EncounterSO
  {
    public override async Task BeginAsync(MapManager mapManager)
    {
      await mapManager.UIManager.ShowEncounter(mapManager.UIManager.narrativeCanvasRef);
    }
  }
}