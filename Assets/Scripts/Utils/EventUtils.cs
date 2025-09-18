using System;
using UnityEngine;

namespace Utils
{
  [Serializable]
  public class EventChoice
  {
    public string ChoiceText;
    public EventResultType ResultType;
    public int ResultValue;
  }
  public enum EventResultType
  {
    GainGold,      // 골드 획득
    LoseGold,      // 골드 잃음
    GainHP,        // 체력 회복
    LoseHP,        // 체력 잃음
    GainMaxHP,     // 최대 체력 증가
    AddCard,       // 카드 획득
    RemoveCard,    // 카드 제거
    UpgradeCard,   // 카드 강화
    AddRelic,      // 유물 획득
    AddPotion,     // 포션 획득
    StartBattle,   // 전투 시작
    Nothing        // 아무 일도 일어나지 않음
  }
}