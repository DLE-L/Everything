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