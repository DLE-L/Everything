/*
1. CardSystem 만들기: 전투 중 '뽑을 카드 더미', '손', '버린 카드 더미'를 관리할 CardSystem 클래스를 만들어줘.

2. 전투 시작 시 카드 섞기: 전투가 시작되면, 플레이어의 기본 덱(내가 아까 보내준 '타격' 4장, '수비' 4장 등)을 
'뽑을 카드 더미'로 가져와서 무작위로 섞어줘.

3. 5장 뽑아서 손에 보여주기: '뽑을 카드 더미'에서 5장을 뽑아서 '손(Hand)'으로 옮기고, 화면 하단에 이 카드들의 이름이라도 
간단히 보이게 UI를 연결해줘.
*/
using System.Collections.Generic;
using Player;
using UnityEngine;
using Utils;

namespace GameSystem.Scene.Battle
{
  public class CardSystem : MonoBehaviour
  {
    public Queue<string> cards = new();
    public PlayerController player;

    public void Init()
    {
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
      // 1. 플레이어 현재 덱 가져오기      

      // 2. 덱 섞기
      
      // 3. 덱에서 5장 뽑기
      DrawCard(5);
      // 손에 보여주기
    }

    public void DrawCard(int Count)
    {
      for (int i = 0; i < Count; i++)
      {
       //  = cards.Dequeue();
      }
    }

    private void Shuffle()
    {
      System.Random random = new();

    //   int deckCount = deck.Cards.Count;
    //   for (int i = 0; i < deckCount - 1; i++)
    //   {
    //     var randomIndex = random.Next(i, deckCount);
    //     (deck.Cards[i], deck.Cards[randomIndex]) = (deck.Cards[randomIndex], deck.Cards[i]);
    //   }
    //   foreach (var card in deck.Cards)
    //   {
    //     cards.Enqueue(card);
    //   }
    }
  }
}