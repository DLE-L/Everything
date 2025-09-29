using UnityEngine;
using System.Collections.Generic;

namespace Units
{  
  public abstract class UnitActionSO : ScriptableObject
  {
    public abstract void Execute(Unit user, List<Unit> allAllies, List<Unit> allEnemies);  
  }
}