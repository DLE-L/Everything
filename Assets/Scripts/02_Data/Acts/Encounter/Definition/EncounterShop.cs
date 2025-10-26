using System.Threading.Tasks;
using GamePlay.Map;
using UnityEngine;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Shop_", menuName = "MyMenu/Act/Encounter/Shop")]
  public class EncounterShop : EncounterSO
  {
    public override async Task BeginAsync(MapManager mapManager, Node node)
    {
      await mapManager.uiManager.ShowEncounter(mapManager.assetLoader.ShopCanvasRef, node);
    }
  }
}