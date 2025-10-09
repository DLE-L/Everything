using UnityEngine;
using GamePlay.Battle;

namespace Data.Card
{
  [CreateAssetMenu(fileName = "CardType_Skill", menuName = "MyMenu/Item/CardType/Skill")]
  public class CardSkillType : CardTypeSO
  {
    public override void OnCardPlayed(CardSO card, CardManager manager)
    {
      base.OnCardPlayed(card, manager);
    }
  }
}