using System.Collections.Generic;
using Core.Event;
using Data.Collectible.Card;
using TMPro;
using UnityEngine.UI;
using Data.Units;
using UnityEngine;

namespace UIs.Battle
{
  public class PlayerBattleUI : MonoBehaviour // MonoBehaviour 제거 예정
  {
    public Image imgPlayerHp;
    public Image imgPlayerEnergy;

    public TextMeshProUGUI txtPlayerHp;
    public TextMeshProUGUI txtPlayerEnergy;

    public void Init()
    {

    }


    public void SetHP_UI(StatData stat)
    {
      txtPlayerHp.text = $"HP: {stat.HP} / {stat.MaxHP} ";
    }
    public void SetEnergy_UI(StatData stat)
    {
      txtPlayerEnergy.text = $"Energy: {stat.Energy} / {stat.MaxEnergy} ";
    }
    
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