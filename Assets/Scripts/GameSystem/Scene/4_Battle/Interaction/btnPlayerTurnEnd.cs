using System;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class btnPlayerTurnEnd : MonoBehaviour
  {
    public event Action OnClickPlayerTurnEnd;

    public void OnClickTurnEnd()
    {
      BattleManager manager = GameSystem.Instance.Battle;
      manager.FSM.ChangeState(new TurnEndState(manager, manager.FSM, TurnOwner.Enemy));
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnClickPlayerTurnEnd?.Invoke();
      };

      OnClickPlayerTurnEnd += OnClickTurnEnd;      
    }

    void OnDisable()
    {
      OnClickPlayerTurnEnd -= OnClickTurnEnd;
    }
  
  }
}