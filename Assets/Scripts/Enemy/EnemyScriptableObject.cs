using GameSystem;
using UnityEngine;

namespace Enemy
{
  [CreateAssetMenu(fileName = "Enemy", menuName = "MyMenu/Enemy")]
  public class EnemyScriptableObject : ScriptableObject
  {
    [Header("Enemy Stat")]
    public StatData statData;
    [Space(10)]    
    public string Name;
  }
}