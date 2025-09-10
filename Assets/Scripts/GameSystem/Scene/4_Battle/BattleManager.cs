using UnityEngine;
using System.Collections.Generic;
using GameSystems.Scene.Battle.States;
using Player;
using Utils;
using System;

namespace GameSystems.Scene.Battle
{
  public class BattleManager : MonoBehaviour
  {
    public List<BattleCardData> DrawPile = new();
    public List<BattleCardData> DiscardPile = new();
    public List<BattleCardData> Hand = new();

    public PlayerController Player { get; private set; }
    public PlayerInventory PlayerInventory => Player.Inventory;
    public PlayerRunState PlayerRunState => Player.Stat.RunState;

    public BattleStateSystem StateSystem { get; private set; }

    private System.Random _random = new();
    public BattleCard[] battleCards;

    public event Action<BattleCardData> OnCardAction;

    private void Awake()
    {
      StateSystem = new BattleStateSystem();
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
      StateSystem.ChangeState(new StateSetup(this, StateSystem));

      foreach (BattleCard battleCard in battleCards)
      {
        battleCard.OnCardClicked += (card) =>
        {
          // 1. 카드 사용
          UseCard(battleCard, card);
        };
      }
    }

    public void Update()
    {
      StateSystem.Execute();
    }

    public void UseCard(BattleCard card, BattleCardData data)
    {
      // 1. 플레이어 코스트 > 에너지 소모 일시 사용
      if (UseEnergy(data.Data.Cost) == false)
      {
        // TODO: 에너지 부족 경고 문구 UI추가
        Debug.Log("에너지 부족");
        return;
      }
      // 2. 사용 카드 효과 발동
      OnCardAction?.Invoke(data);
      Debug.Log($"[카드 사용]: {data.Data.Name}\n[남은 에너지]: {Player.Stat.RunState.CurrentEnergy}");
      // 3. 사용 카드 DiscardPile에 추가
      DiscardHandCard(data);
      // 4. 카드 UI 업데이트
      CardUIUpdate(card, false);
    }

    public void ResetEnergy()
    {
      PlayerRunState.CurrentEnergy = PlayerRunState.MaxEnergy;
    }

    public bool UseEnergy(int cost)
    {
      if (PlayerRunState.CurrentEnergy < cost)
      {
        return false;
      }
      PlayerRunState.CurrentEnergy -= cost;
      return true;
    }

    public void DiscardHandCard(BattleCardData battleCard)
    {
      DiscardPile.Add(battleCard);
      Hand.Remove(battleCard);
    }

    public void DiscardHandCardAll()
    {
      int handCount = Hand.Count;
      for (int i = 0; i < handCount; i++)
      {
        DiscardPile.Add(Hand[0]);
        Hand.RemoveAt(0);
      }
    }

    public void CardUIUpdate(BattleCard card, bool active)
    {      
      card.UpdateUI();
      card.gameObject.SetActive(active);
    }

    public void DrawCard(int amount)
    {
      for (int i = 0; i < amount; i++)
      {
        if (DrawPile.Count == 0)
        {
          DrawPile = DiscardPile;
          DiscardPile = new();
          Shuffle(DrawPile);
        }

        Hand.Add(DrawPile[0]);
        battleCards[i].BattleCardData = DrawPile[0];
        CardUIUpdate(battleCards[i], true);
        DrawPile.RemoveAt(0);
      }
    }

    public void Shuffle<T>(List<T> deck)
    {
      for (int i = 0; i < deck.Count - 1; i++)
      {
        var randomIndex = _random.Next(i, deck.Count);
        (deck[i], deck[randomIndex]) = (deck[randomIndex], deck[i]);
      }
    }

    public void GetPlayerDeck()
    {
      PlayerAccountData account = PlayerInventory.PlayerData;
      if (account == null) return;
      Dictionary<string, int> data = account.GetCurrentCardDeck();      
      foreach (var cardInfo in data)
      {
        for (int i = 0; i < cardInfo.Value; i++)
        {          
          DrawPile.Add(new BattleCardData(cardInfo.Key, $"{cardInfo.Key}_{i}"));
        }
      }
    }

    public void ChangePlayerStartState() => StateSystem.ChangeState(new StatePlayerStart(this, StateSystem));
    public void ChangePlayerTurnState() => StateSystem.ChangeState(new StatePlayerTurn(this, StateSystem));
    public void ChangePlayerEndState() => StateSystem.ChangeState(new StatePlayerEnd(this, StateSystem));

    public void ChangeEnemyStartState() => StateSystem.ChangeState(new StateEnemyStart(this, StateSystem));
    public void ChangeEnemyTurnState() => StateSystem.ChangeState(new StateEnemyTurn(this, StateSystem));  
    public void ChangeEnemyEndState() => StateSystem.ChangeState(new StateEnemyEnd(this, StateSystem));  
    
  }
}

/*
[전투 시작]
1. Setup
2. Player Turn
3. Enemy Turn

2~3반복
[전투 종료] - Win, Loose
3-1. Win : 게임씬으로 복귀
3-2. Loose : Lobby로 퇴장

*/