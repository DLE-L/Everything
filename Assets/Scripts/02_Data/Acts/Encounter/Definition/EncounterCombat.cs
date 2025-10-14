using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Event;
using UnityEngine;
using Data.Units;
using Data.Rarity;
using GamePlay.Map;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Combat_", menuName = "MyMenu/Act/Encounter/Combat")]
  public class EncounterCombat : EncounterSO
  {
    public RaritySO Rarity;
    public List<EnemySO> Enemies;
    
    public override async Task BeginAsync(MapManager mapManager)
    {
      GameSystem.Instance.CurrentEncounter = this;
      await GameSystem.Instance.Scene.LoadSceneBattleAsync();
    }
  }
}