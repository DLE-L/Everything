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
    private readonly System.Random random = new();
    private UI_UnitHP _uiUnitHP;
    public List<CardSO> Cards { get; private set; }
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
      Cards = new List<CardSO>(enemySo.Deck);
      _uiUnitHP.InitializeUnitHPBar(this);
    }

    protected override void HandleTurnStart(TurnOwner turnOwner)
    {
      base.HandleTurnStart(turnOwner);
      if (turnOwner == Team)
      {
        EnemyNextIntent();
      }
    }
    public void EnemyNextIntent()
    {
      var rand = random.Next(0, Cards.Count);
      var card = Cards[rand];
      NextCard = card;
      Debug.Log($"{name}_Next Card:{card.name}");
    }
  }
}