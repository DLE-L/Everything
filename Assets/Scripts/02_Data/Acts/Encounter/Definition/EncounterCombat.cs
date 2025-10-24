using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Event;
using UnityEngine;
using Data.Units;
using Data.Rarity;
using Data.Reward;
using GamePlay.Map;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Combat_", menuName = "MyMenu/Act/Encounter/Combat")]
  public class EncounterCombat : EncounterSO
  {
    public RaritySO Rarity;
    public RewardStrategySO RewardStrategy;
    public List<EnemySO> Enemies;
    
    public override async Task BeginAsync(MapManager mapManager, Node node)
    {
      GameSystem.Instance.CurrentEncounter = this;
      //GameSystem.Instance.Map.mapUIManager.CanvasMapActive(false);
      await GameSystem.Instance.Scene.LoadSceneBattleAsync();
    }
  }
}