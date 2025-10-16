using Core;
using GamePlay.Battle;
using GamePlay.Battle.State;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Battle
{
  public class btnPlayerTurnEnd : MonoBehaviour
  {
    public void OnClickTurnEnd(PointerEventData data)
    {
      BattleManager manager = GameSystem.Instance.Battle;
      manager.Fsm.ChangeState(new StateTurnEnd(manager, manager.Fsm, TurnOwner.EnemyTeam));
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