using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Data.Collectible.Card;
using Core.Event;

namespace GamePlay.Battle
{
  public class CardManager : MonoBehaviour
  {
    public const int MAX_COUNT_HAND = 10;
    
    public List<RuntimeCard> DrawPile;
    public List<RuntimeCard> DiscardPile;
    public List<RuntimeCard> Hand;
    public List<RuntimeCard> ExhaustPile;
     
    public List<string> Draws;
    public List<string> Discards;
    public List<string> Hands;
    public List<string> Exhausts;

    private readonly System.Random _random = new();

    private void Update()
    {
      Draws = DrawPile.Select(x => x.Data.Name).ToList();
      Discards = DiscardPile.Select(x => x.Data.Name).ToList();
      Hands = Hand.Select(x => x.Data.Name).ToList();
      Exhausts = ExhaustPile.Select(x => x.Data.Name).ToList();
    }

    public void Init()
    {
      var DeckList = RunSystem.Instance.PlayerData.Deck;
      
      DrawPile = new List<RuntimeCard>(DeckList);
      Hand = new List<RuntimeCard>();
      DiscardPile = new List<RuntimeCard>();

      DrawPileShuffle();
    }

    private void HandlePlayerTurnStart()
    {
      TurnStartDiscardHand();
      Draw(5);
    }

    private void Draw(int amount)
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
        //Debug.Log($"{runtimeCard.Data.Name} is Draw");
      }
    }

    private void Discard(RuntimeCard cardToDiscard)
    {
      if (Hand.Remove(cardToDiscard))
      {
        if (cardToDiscard.Data.Exhaust) ExhaustPile.Add(cardToDiscard);
        else DiscardPile.Add(cardToDiscard);

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

    // 덱 섞기
    private void Reshuffle()
    {
      DrawPile.AddRange(DiscardPile);
      DiscardPile.Clear();
      DrawPileShuffle();
    }

    private void DrawPileShuffle()
    {
      var shuffleList = DrawPile.OrderBy(x => _random.Next()).ToList();
      DrawPile.Clear();
      DrawPile.AddRange(shuffleList);
    }

    private void OnEnable()
    {
      BattleEvent.OnPlayerTurnStart += HandlePlayerTurnStart;
      
      BattleEvent.OnRequestDraw += Draw;
    }

    private void OnDisable()
    {
      BattleEvent.OnPlayerTurnStart -= HandlePlayerTurnStart;
      
      BattleEvent.OnRequestDraw -= Draw;
    }
  }
}