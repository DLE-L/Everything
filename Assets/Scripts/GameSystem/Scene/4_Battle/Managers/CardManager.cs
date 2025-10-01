using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;
using Utils;


namespace GameSystems.Scene.Battle
{
  public class CardManager
  {
    public List<CardSO> DrawPile;
    public List<CardSO> DiscardPile;
    public List<CardSO> Hand;    

    private System.Random _random = new();

    public CardManager(List<CardSO> startingDeck)
    {
      DrawPile = new List<CardSO>(startingDeck);
      Hand = new List<CardSO>();
      DiscardPile = new List<CardSO>();

      Shuffle(DrawPile);
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

        CardSO cardToDraw = DrawPile[0];
        DrawPile.RemoveAt(0);
        Hand.Add(cardToDraw);
      }

      BattleEvent.RaiseHandUpdated(Hand);
    }

    public void Discard(CardSO cardToDiscard)
    {
      if (Hand.Remove(cardToDiscard))
      {
        DiscardPile.Add(cardToDiscard);
      }
    }

    public void DiscardAllHand()
    {
      DiscardPile.AddRange(Hand);
      Hand.Clear();
    }

    public void TurnStartDiscardHand()
    {
      for (int cardIndex = Hand.Count - 1; cardIndex >= 0; cardIndex--)
      {
        if (Hand[cardIndex].Retain == false)
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
        CardSO cardToDiscard = Hand[randomIndex];
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

    private void Shuffle(List<CardSO> list)
    {
      list = list.OrderBy(x => _random.Next()).ToList();
    }
  }
}