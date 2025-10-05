using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class btnPlayerTurnEnd : MonoBehaviour
  {
    public void OnClickTurnEnd(PointerEventData data)
    {
      BattleManager manager = GameSystem.Instance.Battle;
      manager.FSM.ChangeState(new TurnEndState(manager, manager.FSM, TurnOwner.EnemyTeam));
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClickTurnEnd;      
    }

    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClickTurnEnd;
    }
  }
}