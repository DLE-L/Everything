using UnityEngine;
using Utils;
using System;
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Game
{
  public class btnBattleEnter : MonoBehaviour
  {
    public event Action OnClickBattleEnter;

    public void OnClickBattle(PointerEventData data)
    {
      EncounterDatabase.CurrentEncounter = EncounterDatabase.encounters["Encounter_Goblin_Easy_01"];
      SystemEvent.RaiseSceneLoadStart("4_Battle", UnityEngine.SceneManagement.LoadSceneMode.Additive);
      // TODO: 게임 시작
      // 1. 맵 생성
      // 2. 플레이어 배치
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClickBattle;
    }

    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClickBattle;
    }
  }
}