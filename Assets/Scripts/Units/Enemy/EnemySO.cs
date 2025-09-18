using Utils;
using UnityEngine;
using System.Collections.Generic;
using Card;


namespace Units.Enemy
{
  [CreateAssetMenu(fileName = "Enemy", menuName = "MyMenu/Enemy")]
  public class EnemySO : ScriptableObject
  {
    [Header("Enemy Identity")]
    public string EnemyId;

    [Space(10)]
    [Header("Enemy Stat")]
    public StatData Stat;
    public List<CardSO> AbilityCards;
  }
}