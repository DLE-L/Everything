using System.Collections.Generic;
using UnityEngine;
using Data.Character;
using Data.Rarity;

namespace Data.Act.Encounter
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