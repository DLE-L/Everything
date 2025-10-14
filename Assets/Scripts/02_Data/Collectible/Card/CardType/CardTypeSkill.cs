using UnityEngine;
using GamePlay.Battle;

namespace Data.Collectible.Card
{
  [CreateAssetMenu(fileName = "CardType_Skill", menuName = "MyMenu/Card/CardType/Skill")]
  public class CardTypeSkill : CardTypeSO
  {
    public override void OnCardPlayed(CardSO card, CardManager manager)
    {
      base.OnCardPlayed(card, manager);
    }
  }
}