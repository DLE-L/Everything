using System.Collections.Generic;
using UnityEngine;
using GamePlay.Battle;
using Data.Collectible.Card;
using Data.Units;
using UI.Units;

namespace GamePlay.Units
{
  public class EnemyController : Unit
  {
    private BattleManager _battleManager;
    private readonly System.Random _random = new();
    private UI_UnitHP _uiUnitHP;
    private List<CardSO> _cards;
    public CardSO NextCard { get; private set; }

    void Awake()
    {
      Team = TurnOwner.EnemyTeam;
      _uiUnitHP ??= GetComponentInChildren<UI_UnitHP>();
    }

    public void DataSetting(EnemySO enemySo, BattleManager manager)
    {
      _battleManager = manager;
      gameObject.name = enemySo.name;
      Stat.MaxHP = enemySo.MaxHP;
      Stat.HP = enemySo.MaxHP;
      _cards = new List<CardSO>(enemySo.Deck);
      _uiUnitHP.InitializeUnitHPBar(this);
    }

    protected override void HandleTurnStart(TurnOwner turnOwner)
    {
      base.HandleTurnStart(turnOwner);
      if (turnOwner != Team) return;
      
      EnemyNextIntent();
    }
    private void EnemyNextIntent()
    {
      var rand = _random.Next(0, _cards.Count);
      var card = _cards[rand];
      NextCard = card;
      Debug.Log($"{name}_Next Card:{card.name}");
    }
  }
}