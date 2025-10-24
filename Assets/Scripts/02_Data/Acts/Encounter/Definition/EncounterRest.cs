using System.Threading.Tasks;
using GamePlay.Map;
using UnityEngine;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Rest_", menuName = "MyMenu/Act/Encounter/Rest")]
  public class EncounterRest : EncounterSO
  {
    public override async Task BeginAsync(MapManager mapManager, Node node)
    {
      await mapManager.uiManager.ShowEncounter(mapManager.assetLoader.restCanvasRef, node);
    }
  }
}