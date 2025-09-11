using GameSystems.Scene.Battle;
using UnityEngine;


namespace Units.Enemy
{
  public class EnemyController : Unit
  {
    public BattleManager battleManager;
    public EnemySO enemySO;

    void Awake()
    {
      battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      Stat = enemySO.Stat;
    }
  }
}