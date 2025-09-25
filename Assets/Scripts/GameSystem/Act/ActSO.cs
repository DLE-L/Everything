using GameSystems.Act.Encounter;
using UnityEngine;
using System.Collections.Generic;

namespace GameSystems.Act
{
  [CreateAssetMenu(fileName = "Act", menuName = "MyMenu/Act/Act")]
  public class ActSO : ScriptableObject
  {
    public List<EncounterSO> CommonEncounters;
    public List<EncounterSO> EliteEncounters;
    public EncounterSO BossEncounter;

    public int MaxShopCount = 2;
    public int MaxRestCount = 2;
    public int MaxEliteCount = 3;
  }
}
