using UnityEngine;
using GamePlay.Battle;

namespace Data.Card
{
  [CreateAssetMenu(fileName = "CardType_Attack", menuName = "MyMenu/Item/CardType/Attack")]
  public class CardAttackType : CardTypeSO
  {
    public override void OnCardPlayed(CardSO card, CardManager manager)
    {
      base.OnCardPlayed(card, manager);      
    }
  }
}