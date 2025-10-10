using UnityEngine;
using GamePlay.Battle;
using Core;

namespace Data.Card
{
  [CreateAssetMenu(fileName = "CardType_Power", menuName = "MyMenu/Card/CardType/Power")]
  public class CardTypePower : CardTypeSO
  {
    public override void OnCardPlayed(CardSO card, CardManager manager)
    {
      manager.Hand.Remove(card);      
      GameSystem.Instance.RemoveCardFromDeckPermanently(card);
    }
  }
}