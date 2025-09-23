using Utils;
using UnityEngine;
using System.Collections.Generic;
using Item;


namespace Units.Enemy
{
  [CreateAssetMenu(fileName = "Enemy", menuName = "MyMenu/Enemy")]
  public class EnemySO : ScriptableObject
  {
    [Space(10)]
    [Header("Enemy Stat")]
    public StatData Stat;
    public List<CardSO> AbilityCards;
  }
}