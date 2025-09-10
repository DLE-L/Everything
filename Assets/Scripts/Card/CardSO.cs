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
    [Header("Card Data")]
    public CardType CardType;
    public string Name;
    public int Damage;
    public int Cost;
    [TextArea(order = 300)] public string Explain;
  }
}