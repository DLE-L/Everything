using GameSystems;
using GameSystems.Scene.Battle;
using UnityEngine;

namespace Item
{
  [CreateAssetMenu(fileName = "CardType_Power", menuName = "MyMenu/Item/CardType/Power")]
  public class CardPowerType : CardTypeSO
  {
    public override void OnCardPlayed(CardSO card, CardManager manager)
    {
      manager.Hand.Remove(card);      
      GameSystem.Instance.RemoveCardFromDeckPermanently(card);
    }
  }
}