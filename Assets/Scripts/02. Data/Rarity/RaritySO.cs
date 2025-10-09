using UnityEngine;

namespace Data.Rarity
{
  public abstract class RaritySO : ScriptableObject
  {
    public string Name;
    public Color Frame;
    public Sprite Icon;
  }
}