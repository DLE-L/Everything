using System;
using System.Collections.Generic;
using UnityEngine;


namespace Item
{
  [CreateAssetMenu(fileName = "Relic", menuName = "MyMenu/Relic")]
  public class RelicSO : ScriptableObject
  {
    public string Name;
    public string Description;
    public List<RelicEffect> Effects;
  }

  [Serializable]
  public class RelicEffect
  {
    public int Value;
  }
}