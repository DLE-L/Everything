using UnityEngine;
using System.Collections.Generic;
using GameSystems.Scene.Battle.States;
using Utils;
using System;
using System.Linq;
using Units.Player;
using Units.Enemy;
using Units;


namespace GameSystems.Scene.Battle
{
  public class BattleManager : MonoBehaviour
  {
    public List<BattleCardData> DrawPile = new();
    public List<BattleCardData> DiscardPile = new();
    public List<BattleCardData> Hand = new();

    public PlayerController Player { get; private set; }
    public PlayerInventory PlayerInventory => Player.Inventory;
    public StatData PlayerStat => Player.Stat;
    public List<EnemyController> Enemies = new();

    public BattleStateSystem StateSystem { get; private set; }
    private System.Random _random = new();
    public BattleCard[] battleCards;
    public event Action<BattleCardData> OnCardActionDealDamage;

    private void Awake()
    {
      StateSystem = new BattleStateSystem();
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
      Enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyController>()).ToList();
    }

    private void Start()
    {
      StateSystem.ChangeState(new StateSetup(this, StateSystem));

      Player.OnDeath += OnUnitDied;
      foreach (EnemyController enemy in Enemies)
      {
        enemy.OnDeath += OnUnitDied;
      }

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

    public void OnUnitDied(Unit unit)
    {
      if (unit is EnemyController)
      {
        Enemies.Remove(unit as EnemyController);
        if (Enemies.Count == 0)
        {
          // ChangeState(new WinState(this, StateSystem));
        }
      }
      else if (unit is PlayerController)
      {
        // ChangeState(new LoseState(this, StateSystem));
      }
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
      CardEffectActvie(data);
      Debug.Log($"[카드 사용]: {data.Data.CardName}");
      // 3. 사용 카드 DiscardPile에 추가
      DiscardHandCard(data);
      // 4. 카드 UI 업데이트
      CardUIUpdate(card, false);
    }

    public void CardEffectActvie(BattleCardData data)
    {
      switch (data.Data.CardEffectType)
      {
        case CardEffectType.DealDamage:
          OnCardActionDealDamage?.Invoke(data);
          break;
        case CardEffectType.GainBlock:
          Player.GainBlock(data.Data.EffectValue);
          break;
      }
    }

    public void ResetBlock()
    {
      Debug.Log($"Player Turn End. Block Reset");
      PlayerStat.Block = 0;
    }

    public void ResetEnergy()
    {
      PlayerStat.Energy = PlayerStat.MaxEnergy;
    }

    public bool UseEnergy(int cost)
    {
      if (PlayerStat.Energy < cost)
      {
        return false;
      }
      PlayerStat.Energy -= cost;
      Debug.Log($"남은 에너지: {PlayerStat.Energy}");
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

    public void ChangePlayerTurnState() => StateSystem.ChangeState(new StatePlayerTurn(this, StateSystem));
    public void ChangeEnemyTurnState() => StateSystem.ChangeState(new StateEnemyTurn(this, StateSystem));
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