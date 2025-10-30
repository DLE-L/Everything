using UnityEngine;
using GamePlay.Battle;

namespace Data.Collectible.Card
{
  public abstract class CardTypeSO : ScriptableObject
  {
    public string TypeName;
    public Color FrameColor;
    public Sprite Icon;
    public string Description;
  } 
}