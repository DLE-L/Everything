using UnityEngine;
using Utils;
using System;

namespace GameSystems.Scene.Game
{
  public class btnBattleEnter : MonoBehaviour
  {
    public event Action OnClickBattleEnter;

    public void OnClickBattle()
    {
      EncounterDatabase.CurrentEncounter = EncounterDatabase.encounters["Encounter_Goblin_Easy_01"];
      GameSystem.Instance.LoadBattleScene();
      // TODO: 게임 시작
      // 1. 맵 생성
      // 2. 플레이어 배치
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnClickBattleEnter?.Invoke();
      };

      OnClickBattleEnter += OnClickBattle;
    }
    void OnDisable()
    {
      OnClickBattleEnter -= OnClickBattle;
    }
  }
}