using UnityEngine;
using GamePlay.Battle;
using Core;
using Core.Event;
using Data.Units;

namespace Data.Collectible.Card
{
  [CreateAssetMenu(fileName = "CardType_Power", menuName = "MyMenu/Card/CardType/Power")]
  public class CardTypePower : CardTypeSO
  {
    public override void OnCardPlayed(CardSO card, CardManager manager)
    {
      manager.Hand.Remove(card);
      BattleEvent.RaisePlayPowerCard(card);
    }
  }
}