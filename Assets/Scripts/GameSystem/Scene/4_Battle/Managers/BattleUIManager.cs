using System.Collections.Generic;
using Item;
using UnityEngine;

namespace GameSystems.Scene.Battle
{
  public class BattleUIManager : MonoBehaviour
  {

    public void UpdateHandUI(List<CardSO> hand)
    {

    }
    void OnEnable()
    {
      BattleEvent.OnHandUpdated += UpdateHandUI;
    }
    void OnDisable()
    {
      BattleEvent.OnHandUpdated -= UpdateHandUI;
    }

    
  }
}