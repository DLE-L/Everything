using Card.Data;
using UnityEngine;

namespace Card
{
  [CreateAssetMenu(fileName = "Card", menuName = "MyMenu/Card")]
  public class CardScriptableObject : ScriptableObject
  {
    [Header("Card Data")]
    public CardType cardType;
    public string Name;
    public int Damage;

    [TextArea(order = 300)] public string Explain;
  }
}