using System.Collections.Generic;
using GameSystems.Scene.Game;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace GameSystems.Act.Encounter
{
  [CreateAssetMenu(fileName = "EncounterPool", menuName = "MyMenu/EncounterPool")]
  public class EncounterPoolSO : ScriptableObject
  {
    public List<EncounterSO> Encounters;
  }
}