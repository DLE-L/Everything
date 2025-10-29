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
    private void OnClickTurnEnd(PointerEventData data)
    {
      var battleManager = GameSystem.Instance.Battle;
      battleManager.Fsm.ChangeState(new StateTurnEnd(battleManager, battleManager.Fsm, TurnOwner.PlayerTeam));
      //manager.Fsm.ChangeState(new StateVictory(manager, manager.Fsm));
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