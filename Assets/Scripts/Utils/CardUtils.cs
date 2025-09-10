using System;
using Card;
using GameSystems;

namespace Utils
{
  public enum CardType
  {
    Attack,
    Deffence,
    Skill,
  }

  [Serializable]
  public class BattleCardData
  {
    public CardSO Data;
    public string BattleCardID;

    public BattleCardData(string cardObjectID, string cardId)
    {
      Data = CardDatabase.GetCardData(cardObjectID);
      BattleCardID = cardId;
    }
  }
}
