using System;
using GameSystems.Scene.Battle;
using UnityEngine;
using Utils;


namespace Units.Enemy
{
  public class EnemyController : Unit
  {
    public BattleManager battleManager;
    public EnemySO enemySO;
    public Action<EnemyController> OnEnemyClicked;

    void Awake()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnEnemyClicked?.Invoke(this);
      };
    }

    public void Init()
    {
      battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      Stat = enemySO.Stat;
    }
  }
}