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

    void Start()
    {
      if (battleManager == null)
      {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      battleManager.ChangeEnemyTurnState();
    }
  }
}