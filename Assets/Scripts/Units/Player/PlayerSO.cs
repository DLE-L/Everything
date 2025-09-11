using Utils;
using UnityEngine;

namespace Units.Player
{
  [CreateAssetMenu(fileName = "Player", menuName = "MyMenu/Player")]
  public class PlayerSO : ScriptableObject
  {
    [Header("Player Stat")]
    public PlayerAccountData Stat;    
  }
}