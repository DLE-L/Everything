using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using GamePlay.Battle;
using Data.Collectible.Card;
using Data.Target;
using Data.Units;
using UIs.Units;

namespace GamePlay.Units
{
  public class EnemyController : Unit
  {
    private BattleManager _battleManager;
    private readonly System.Random _random = new();
    private UI_UnitHP _uiUnitHP;
    private List<RuntimeCard> _cards;
    public RuntimeCard NextCard { get; private set; }

    void Awake()
    {
      Team = TurnOwner.EnemyTeam;
      _uiUnitHP ??= GetComponentInChildren<UI_UnitHP>();
    }

    public void DataSetting(EnemySO enemySo, BattleManager manager)
    {
      _battleManager = manager;
      gameObject.name = enemySo.name;
      
      Stat ??= new StatData();
      Stat.MaxHP = enemySo.MaxHP;
      Stat.HP = enemySo.MaxHP;
      
      _cards = enemySo.Deck.Select(cardSo => new RuntimeCard(cardSo)).ToList();
      _uiUnitHP.InitializeUnitHPBar(this);
    }

    public async Task<bool> EnemyActing()
    {
      return await CardUsedOnTarget(NextCard);
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
      Debug.Log($"{name}_Next Card:{card.Data.Name}");
    }
    private async Task<bool> CardUsedOnTarget(RuntimeCard card)
    {
      try
      {
        foreach (var effect in card.Data.Effects)
        {
          var targetStrategy = effect.Target;
          var context = new TargetingContext(
            this,
            _battleManager.UnitManager.PlayerTeam,
            _battleManager.UnitManager.EnemyTeam,
            null
          );

          var targets = await targetStrategy.FindTargetsAsync(context);
          foreach (var targetUnit in targets)
          {
            if (targetUnit.IsDie) return false;
            effect.Effect.Execute(this, targetUnit);  
          }
        }

        return true;
      }
      catch (Exception e)
      {
        Debug.LogWarning($"CardUsedOnTarget warning: {e.Message}");
        return false;
      }
    }
  }
}