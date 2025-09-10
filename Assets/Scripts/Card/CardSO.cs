using System;
using Utils;
using UnityEngine;

namespace GameSystems
{
  [Serializable]
  [CreateAssetMenu(fileName = "Card", menuName = "MyMenu/Card")]
  public class CardSO : ScriptableObject
  {
    [Header("Card Identity")]
    public string CardId;

    [Space(20)]
    [Header("Card Type")]
    public CardType CardType;
    public CardEffectType CardEffectType;
    public int EffectValue;

    [Space(20)]
    [Header("Card Info")]    
    public string CardName;        
    public int Cost;
    // public Sprite CardImage; // TODO: 카드 이미지 추가
    [TextArea(order = 300)] public string Description;
    
  }
}