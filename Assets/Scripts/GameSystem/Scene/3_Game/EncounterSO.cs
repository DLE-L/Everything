using System.Collections.Generic;
using UnityEngine;
using Units.Enemy;

namespace GameSystems.Scene.Game
{
  public enum GameLevelType
  {
    Easy,
    Normal,
    Hard  
  }

  [CreateAssetMenu(fileName = "Encounter", menuName = "MyMenu/Encounter")]
  public class EncounterSO : ScriptableObject
  {
    public GameLevelType GameLevelType;
    public List<EnemySO> EnemyList;
  }
}