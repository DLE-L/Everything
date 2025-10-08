using System.Collections.Generic;
using Units;
using UnityEngine;

namespace GameSystems.Act
{
  [CreateAssetMenu(fileName = "NewCombat", menuName = "MyMenu/Act/Encounter/Combat")]
  public class CombatEncounter : EncounterSO
  {
    public RaritySO Rarity;
    public List<EnemySO> Enemies;

    public override void BeginEncounter()
    {
      throw new System.NotImplementedException();
    }
  }
}