using System.Collections;
using System.Collections.Generic;
using GameSystems.Scene.Battle;
using GameSystems.Scene.Battle.States;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace GameSystems.Scene.Bttle
{
  public class btnPlayerTurnEnd : InteractableBase
  {
    public BattleManager battleManager;

    public Queue<IBattleState> queue = new();

    void Start()
    {
      if (battleManager == null)
      {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      }

      queue.Enqueue(new StatePlayerTurn(battleManager, battleManager.StateSystem));
      queue.Enqueue(new StatePlayerEnd(battleManager, battleManager.StateSystem));
      queue.Enqueue(new StateEnemyStart(battleManager, battleManager.StateSystem));
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      var que = queue.Dequeue();
      battleManager.StateSystem.ChangeState(que);
      queue.Enqueue(que);
    }
  }
}