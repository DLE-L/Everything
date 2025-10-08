using GameSystems.Act;
using UnityEngine;
using System.Collections.Generic;

namespace GameSystems.Act
{
  [CreateAssetMenu(fileName = "Act", menuName = "MyMenu/Act/Act")]
  public class ActSO : ScriptableObject
  {
    public List<EncounterSO> Encounters;
    public EncounterSO BossEncounter;
    
    [Space(10)]
    public int MaxShopCount = 2;
    public int MaxRestCount = 2;
    public int MaxEliteCount = 3;

    [Space(10)]    
    public EncounterTypeSO CombatType;
    public EncounterTypeSO ShopType;
    public EncounterTypeSO RestType;
    public RaritySO EliteRarity;
    
  }
}
