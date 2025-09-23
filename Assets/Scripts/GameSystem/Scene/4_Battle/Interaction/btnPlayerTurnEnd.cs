using System;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class btnPlayerTurnEnd : MonoBehaviour
  {
    public BattleManager battleManager;

    public event Action OnClickPlayerTurnEnd;

    void Start()
    {
      if (battleManager == null)
      {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      }
    }

    public void OnClickTurnEnd()
    {
      battleManager.ChangePlayerTurnState();
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