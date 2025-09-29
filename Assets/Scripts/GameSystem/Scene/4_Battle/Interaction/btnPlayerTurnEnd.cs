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