using Core;
using Core.Event;
using Data.Collectible.Card;
using Data.Reward;
using UnityEngine;

namespace Data.Units
{
  public static class PlayerRunAction
  {
    private static PlayerRunData _runData => GameSystem.Instance.Run.Player.RunData;
    
    // TODO: 데미지, 회복 효과 발생시 받는 메서드 추가  
      
    private static void RemoveCardFromDeckPermanently(RuntimeCard cardToRemove)
    {
      var permanentDeck = _runData.Deck;
      if (permanentDeck.Remove(cardToRemove))
      {
        Debug.Log($"[{cardToRemove.Data.Name}] card permanent deck remove");
      }
      else
      {
        Debug.LogError($"[{cardToRemove.Data.Name}] card is not existing");
      }
    }
    
    private static void GrantsReward(RewardResult reward)
    {
      foreach (var cardSO in reward.Cards)
      {
        var runtimeCard = new RuntimeCard(cardSO);
        _runData.Deck.TryGetValue(runtimeCard, out var count);
        _runData.Deck[runtimeCard] = count + 1;
      }

      foreach (var relicSO in reward.Relics)
      {
        if (_runData.Relics.Add(relicSO)) continue;
        
        Debug.LogError($"[{relicSO.name}] is all ready existed");
        break;
      }
      
      _runData.RunStateGold += reward.Gold;
    }
    
    public static void Init()
    {
      BattleEvent.OnBattleStart += SubscribeBattleEvents;
      BattleEvent.OnBattleEnd += UnsubscribeBattleEvents;
      SystemEvent.OnStartNewRun += SubscribeRunEvents;
      SystemEvent.OnEndRun += UnsubscribeRunEvents;
    }

    public static void DeInit()
    {
      BattleEvent.OnBattleStart -= SubscribeBattleEvents;
      BattleEvent.OnBattleEnd -= UnsubscribeBattleEvents;
      SystemEvent.OnStartNewRun -= SubscribeRunEvents;
      SystemEvent.OnEndRun -= UnsubscribeRunEvents;
    }
    
    private static void SubscribeRunEvents()
    {
      SystemEvent.OnGrantsReward +=  GrantsReward; 
    }

    private static void UnsubscribeRunEvents()
    {
      SystemEvent.OnGrantsReward -=  GrantsReward;
    }

    private static void SubscribeBattleEvents()
    {
      BattleEvent.OnPlayPowerCard += RemoveCardFromDeckPermanently;
    }
    
    private static void UnsubscribeBattleEvents()
    {
      BattleEvent.OnPlayPowerCard -= RemoveCardFromDeckPermanently;
    }
  }
}