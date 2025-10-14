using UnityEngine;
using GamePlay.Battle;

namespace Data.Collectible.Card
{
  [CreateAssetMenu(fileName = "CardType_Attack", menuName = "MyMenu/Card/CardType/Attack")]
  public class CardTypeAttack : CardTypeSO
  {
    public override void OnCardPlayed(CardSO card, CardManager manager)
    {
      base.OnCardPlayed(card, manager);      
    }
  }
}