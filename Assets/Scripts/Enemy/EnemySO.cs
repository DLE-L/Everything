using Utils;
using UnityEngine;

namespace Enemy
{  
  [CreateAssetMenu(fileName = "Enemy", menuName = "MyMenu/Enemy")]
  public class EnemySO : ScriptableObject
  {
    [Header("Enemy Identity")]
    public string EnemyId;
    
    [Space(20)]
    [Header("Enemy Stat")]
    public StatData Stat;    
    
  }
}