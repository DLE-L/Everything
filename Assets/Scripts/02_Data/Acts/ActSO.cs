using UnityEngine;
using System.Collections.Generic;
using Data.Map;
using Data.Rarity;
using Data.Act.Encounter;

namespace Data.Act
{
  [CreateAssetMenu(fileName = "Act_", menuName = "MyMenu/Act")]
  public class ActSO : ScriptableObject
  {
    public List<EncounterSO> Encounters;
    public EncounterSO BossEncounter;

    public List<EncounterFixPointSO> EncounterPoints;
    
    [Space(10)]
    public int MaxShopCount = 2;
    public int MaxRestCount = 2;
    public int MaxEliteCount = 3;

    [Space(10)]    
    public RaritySO EliteRarity;
  }
}
