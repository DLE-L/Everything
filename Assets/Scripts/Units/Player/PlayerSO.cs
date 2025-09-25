using Utils;
using UnityEngine;

namespace Units.Player
{
  [CreateAssetMenu(fileName = "Player", menuName = "MyMenu/Unit/Player")]
  public class PlayerSO : ScriptableObject
  {
    [Header("Player Stat")]
    public PlayerAccountData Stat;    
  }
}