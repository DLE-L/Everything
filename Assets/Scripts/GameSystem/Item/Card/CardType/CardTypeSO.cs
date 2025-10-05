using GameSystems.Scene.Battle;
using UnityEngine;
namespace Item
{
  public abstract class CardTypeSO : ScriptableObject
  {
    public string TypeName;
    public Color FrameColor;
    public Sprite Icon;
    public string Description;
    public virtual void OnCardPlayed(CardSO card, CardManager manager)
    {
      manager.Discard(card);
      Debug.Log($"{card.name} is Discard");
    }
  } 
}