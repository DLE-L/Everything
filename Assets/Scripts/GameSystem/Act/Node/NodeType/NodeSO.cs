
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GameSystems.Act
{  
  public abstract class NodeSO : ScriptableObject
  {
    public EncounterTypeSO EncounterType;
    public abstract void ExecuteAction(Node owner); 
  }

}