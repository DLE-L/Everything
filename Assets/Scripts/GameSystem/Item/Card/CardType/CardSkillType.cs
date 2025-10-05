using GameSystems.Scene.Battle;
using UnityEngine;

namespace Item
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