using Utils;
using UnityEngine;
using TMPro;
using System;
using GameSystems.Scene.Battle;
using Card;

namespace GameSystems
{
  public class BattleCard : MonoBehaviour
  {
    public BattleCardData BattleCardData;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtCost;

    public event Action<BattleCardData> OnCardClicked;

    public BattleManager battleManager;

    private void Awake()
    {
      battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    private void Start()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnCardClicked?.Invoke(this.BattleCardData);
      };
    }

    public void UpdateUI()
    {
      txtName.text = BattleCardData.CardSO.CardName;
      txtCost.text = $"Cost: {BattleCardData.CardSO.Cost}";
    }
  }
}