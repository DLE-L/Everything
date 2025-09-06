using Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Scene.Battle
{
  public class BattleUI : MonoBehaviour // MonoBehaviour 제거 예정
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
      txtPlayerHp.text = $"HP: {stat.Hp} / {stat.MaxHp} ";
    }
    public void SetEnergy_UI(StatData stat)
    {
      txtPlayerEnergy.text = $"Energy: {stat.Energy} / {stat.MaxEnergy} ";
    }
  }
}