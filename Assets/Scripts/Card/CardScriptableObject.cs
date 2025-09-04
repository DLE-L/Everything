using System;
using Utils;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameSystem
{
  [Serializable]
  [CreateAssetMenu(fileName = "Card", menuName = "MyMenu/Card")]
  public class CardScriptableObject : ScriptableObject
  {
    [Header("Card Data")]
    public CardType CardType;
    public string Name;
    public int Damage;
    public int Cost;
    [TextArea(order = 300)] public string Explain;
    [Space(20)]
    [Header("Card Handler")]
    [NonSerialized] public AsyncOperationHandle handle;  
  }
}