using Card.Data;
using UnityEngine;

namespace Card
{
  [CreateAssetMenu(fileName = "Card", menuName = "MyMenu/Card")]
  public class CardScriptableObject : ScriptableObject
  {
    [Header("Card Data")]
    public CardType CardType;
    public string Name;
    public int Damage;
    public int Cost;

    [TextArea(order = 300)] public string Explain;
  }
}