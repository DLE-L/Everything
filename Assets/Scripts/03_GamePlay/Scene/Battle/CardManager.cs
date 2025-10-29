using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Data.Target;
using Data.Collectible.Card;
using GamePlay.Units;
using Core.Event;
using UIs.Battle;

namespace GamePlay.Battle
{
  public class CardManager
  {
    public const int MAX_COUNT_HAND = 10;
    
    public readonly List<RuntimeCard> DrawPile;
    public readonly List<RuntimeCard> DiscardPile;
    public readonly List<RuntimeCard> Hand;
    public List<CardSO> ExhaustPile;

    private readonly System.Random _random = new();
    
    public CardManager(List<RuntimeCard> startingDeck)
    {
      DrawPile = new List<RuntimeCard>(startingDeck);
      Hand = new List<RuntimeCard>();
      DiscardPile = new List<RuntimeCard>();

      Shuffle(DrawPile);
    }

    public void HandlePlayerTurnStart()
    {
      TurnStartDiscardHand();
      Draw(5);
    }

    public async void EnemyPlayCard(RuntimeCard card, Unit user)
    {
      try
      {
        var battleManager = GameSystem.Instance.Battle;
        BattleEvent.RaiseCardPlay(card);

        foreach (var cardEffect in card.Data.Effects)
        {
          TargetingStrategySO targeting = cardEffect.Target;
          TargetingContext context = new (
            user,
            battleManager.UnitManager.PlayerTeam,
            battleManager.UnitManager.EnemyTeam
          );

          List<Unit> targets = await targeting.FindTargetsAsync(context);

          foreach (Unit target in targets)
          {
            Debug.Log($"Target: {target}");
            cardEffect.Effect.Execute(user, target, battleManager);
          }
        }

        if (user is Player) card.Data.Type.OnCardPlayed(card, this);
        Debug.Log($"{card.Data.Name}: is Play");
      }
      catch (Exception e)
      {
        Debug.LogError($"CardManager PlayCard Error: {e.Message}");
      }
    }

    public void Draw(int amount)
    {
      for (int i = 0; i < amount; i++)
      {
        if (DrawPile.Count == 0)
        {
          if (DiscardPile.Count == 0) return; // 더 이상 뽑을 카드 없음
          Reshuffle();
        }

        var runtimeCard = DrawPile[0];
        DrawPile.RemoveAt(0);
        if (Hand.Count >= MAX_COUNT_HAND)
        {
          DiscardPile.Add(runtimeCard);
          continue;
        }
        Hand.Add(runtimeCard);
        BattleEvent.RaiseCardDraw(runtimeCard);
        Debug.Log($"{runtimeCard.Data.Name} is Draw");
      }
    }

    public void Discard(RuntimeCard cardToDiscard)
    {
      if (Hand.Remove(cardToDiscard))
      {
        DiscardPile.Add(cardToDiscard);
        BattleEvent.RaiseCardDiscard(cardToDiscard);
      }
    }

    public void DiscardAllHand()
    {
      DiscardPile.AddRange(Hand);
      Hand.Clear();
    }

    private void TurnStartDiscardHand()
    {
      for (int cardIndex = Hand.Count - 1; cardIndex >= 0; cardIndex--)
      {
        if (!Hand[cardIndex].Data.Retain)
        {
          Discard(Hand[cardIndex]);
        }
      }
    }

    public void DiscardRandom(int amount)
    {
      for (int i = 0; i < amount && Hand.Count > 0; i++)
      {
        int randomIndex = _random.Next(0, Hand.Count);
        RuntimeCard cardToDiscard = Hand[randomIndex];
        Discard(cardToDiscard); // 기존 Discard 메서드 재사용        
      }
    }

    // 덱 섞기
    private void Reshuffle()
    {
      DrawPile.AddRange(DiscardPile);
      DiscardPile.Clear();
      Shuffle(DrawPile);
    }

    private void Shuffle(List<RuntimeCard> list)
    {
      var cardSoList = list.OrderBy(x => _random.Next()).ToList();
    }
  }
}