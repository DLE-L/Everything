using GameSystem.Utils;
using UnityEngine;

namespace Player
{
  [CreateAssetMenu(fileName = "Player", menuName = "MyMenu/Player")]
  public class PlayerScriptableObject : ScriptableObject
  {
    [Header("Player Stat")]
    public StatData stat;
  }
}