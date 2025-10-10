using Core;
using GamePlay.Battle;
using GamePlay.Battle.State;
using UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Battle
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