using System.Collections.Generic;
using Core.Event;
using Data.Card;
using UnityEngine;

namespace UI.Battle
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