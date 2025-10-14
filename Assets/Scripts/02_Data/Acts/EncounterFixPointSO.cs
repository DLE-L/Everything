using System;
using Data.Act.Encounter;
using UnityEngine;

namespace Data.Act
{
  [CreateAssetMenu(fileName = "FixPoint_",menuName = "MyMenu/Act/FixPoint")]
  public class EncounterFixPointSO : ScriptableObject
  {
    public EncounterSO Encounter;
    [Range(1, 15)] public int FloorIndex;
    [Range(1, 3)] public int NodeIndex;
  }
}