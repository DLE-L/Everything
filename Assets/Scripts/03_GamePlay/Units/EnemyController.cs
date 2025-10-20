using System.Collections.Generic;
using UnityEngine;
using GamePlay.Battle;
using Data.Collectible.Card;
using Data.Units;

namespace GamePlay.Units
{
  public class EnemyController : Unit
  {
    private BattleManager _battleManager;
    public List<CardSO> Cards { get; private set; }
    private readonly System.Random random = new();

    void Awake()
    {
      Team = TurnOwner.EnemyTeam;
    }

    public void DataSetting(EnemySO enemySo, BattleManager manager)
    {
      _battleManager = manager;
      Stat.MaxHP = enemySo.MaxHP;
      Cards = new List<CardSO>(enemySo.Deck);
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
      Debug.Log($"[{name}_Next Card]:{card.name}");
    }
  }
}