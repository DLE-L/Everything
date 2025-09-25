using System;
using GameSystems.Scene.Battle;
using UnityEngine;
using Utils;


namespace Units.Enemy
{
  public class EnemyController : Unit
  {
    public BattleManager battleManager;
    public BattleEnemyData EnemyData;   

    void Awake()
    {

    }

    public void Init()
    {
      battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      Stat = EnemyData.Stat;
    }
  }
}