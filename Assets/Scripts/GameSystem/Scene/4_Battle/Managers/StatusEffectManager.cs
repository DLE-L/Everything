using UnityEngine;
using System.Collections.Generic;
using Units;

namespace GameSystems.Scene.Battle
{
  public class StatusEffectManager : MonoBehaviour
  {
    public void OnProcessTurnStart(List<Unit> team)
    {
      foreach (var unit in team)
      {
        unit.ProcessTurnStartEffects();
      }
    }


    void OnEnable()
    {
      BattleEvent.OnTurnStart += OnProcessTurnStart;
    }

    void OnDisable()
    {
      BattleEvent.OnTurnStart -= OnProcessTurnStart;
    }
  }
}