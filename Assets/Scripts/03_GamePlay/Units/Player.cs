using System;
using System.Threading.Tasks;
using Core;
using Core.Event;
using Data.Target;
using Data.Units;
using GamePlay.Battle;
using UIs.Battle;
using UnityEngine;

namespace GamePlay.Units
{
  public class Player : Unit
  {
    public PlayerRunData PlayerData;

    void Awake()
    {
      Team = TurnOwner.PlayerTeam;
      PlayerData = RunSystem.Instance.PlayerData;
      Stat = PlayerData.Stat;
    }
    
    public async void CardUsedOnTarget(DragCard card, Unit targetUnit)
    {
      try
      {
        var battleManager = GameSystem.Instance.Battle;
        var userUnit = battleManager.UnitManager.PlayerUnit;
        
        var context = new TargetingContext(
          userUnit,
          battleManager.UnitManager.PlayerTeam,
          battleManager.UnitManager.EnemyTeam,
          targetUnit
        );
        
        foreach (var effect in card.RuntimeCard.Data.Effects)
        {
          var targetStrategy = effect.Target;

          var targets = await targetStrategy.FindTargetsAsync(context);
          foreach (var target in targets)
          {
            effect.Effect.Execute(userUnit, target);
          }
        }

        await Task.Yield();
        battleManager.UIManager.AddressableObjectPooler.Release(card.gameObject);
        BattleEvent.RaiseCardPlay(card.RuntimeCard);
      }
      catch (Exception e)
      {
        Debug.LogWarning($"CardUsedOnTarget warning: {e.Message}");
      }
    }
  }
}
