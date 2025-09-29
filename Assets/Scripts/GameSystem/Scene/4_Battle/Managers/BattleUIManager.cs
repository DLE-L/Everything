using System.Collections.Generic;
using Item;
using UnityEngine;

namespace GameSystems.Scene.Battle
{
  public class BattleUIManager : MonoBehaviour
  {
    void OnEnable()
    {
      BattleEvent.OnHandUpdated += UpdateHandUI;
    }

    public void UpdateHandUI(List<CardSO> hand)
    {
      
    }
  }
}