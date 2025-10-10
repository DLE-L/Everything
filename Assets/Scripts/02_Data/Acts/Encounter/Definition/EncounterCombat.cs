using System.Collections.Generic;
using UnityEngine;
using Data.Units;
using Data.Rarity;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Combat_", menuName = "MyMenu/Act/Encounter/Combat")]
  public class EncounterCombat : EncounterSO
  {
    public RaritySO Rarity;
    public List<EnemySO> Enemies;

    public override void BeginEncounter()
    {
      throw new System.NotImplementedException();
    }
  }
}