using UnityEngine;
using System.Collections.Generic;
using GameSystems.Scene.Battle.States;
using Utils;
using System;
using System.Linq;
using Units.Player;
using Units.Enemy;
using Units;
using Card;
using GameSystems.Scene.Game;
using System.Text;

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

    public Unit CurrentUser { get; set; }
    public Unit CurrentTarget { get; set; }

    public BattleStateSystem StateSystem { get; private set; } = new();
    public System.Random random = new();
    public BattleCard[] battleCards;

    public event Action<BattleCard> OnUseCard;

    [SerializeField] private GameObject enemyGameObject;
    [SerializeField] private Transform enemyTransform;


    private void Awake()
    {
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
      Enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyController>()).ToList();
    }

    private void Start()
    {
      // 1. 배틀 입장 - 게임 씬에서 적 정보 획득 및 생성
      BattleEncounter();

      // 2. 배틀 첫 상태 시작
      StateSystem.ChangeState(new StateSetup(this, StateSystem));

      // 3. 플레이어 & 적 이벤트 등록
      Player.OnDeath += OnUnitDied;
      foreach (EnemyController enemy in Enemies)
      {
        enemy.OnDeath += OnUnitDied;
        enemy.OnEnemyClicked += OnEnemyClicked;
      }

      // 4. 카드 이벤트 등록
      foreach (BattleCard battleCard in battleCards)
      {
        battleCard.OnCardClicked += (card) =>
        {
          if (UseEnergy(card.CardSO.Cost) == false)
          {
            // TODO: 에너지 부족 경고 문구 UI추가
            Debug.Log("에너지 부족");
            return;
          }
          UseCard(card.CardSO, CurrentUser, CurrentTarget);
          DiscardHandCard(card);
          CardUIUpdate(battleCard, false);
          // OnUseCard?.Invoke(battleCard);
        };
      }

      EnemyNextCard();
    }

    public void Update()
    {
      StateSystem.Execute();
    }

    public void BattleEncounter()
    {
      List<EnemySO> enemyList = EncounterDatabase.CurrentEncounter.EnemyList;
      int count = 0;
      foreach (EnemySO enemySO in enemyList)
      {
        enemyTransform.position += new Vector3(0, count, 0);
        GameObject go = Instantiate(enemyGameObject, enemyTransform);
        EnemyController controller = go.GetComponent<EnemyController>();
        go.name = enemySO.name + count;
        controller.enemySO = enemySO;
        controller.Init();
        Enemies.Add(controller);
        count++;
      }
    }

    public void EnemyNextCard()
    {
      for (int i = 0; i < Enemies.Count; i++)
      {
        int rand = random.Next(0, Enemies[i].enemySO.AbilityCards.Count);
        CardSO card = Enemies[i].enemySO.AbilityCards[rand];
        Debug.Log($"[{Enemies[i].name}_Next Card]:{card.name}");
      }
    }

    public void OnEnemyClicked(EnemyController enemy)
    {
      CurrentTarget = enemy;
      Debug.Log($"[Select Enemy:{CurrentTarget.name}]");
    }

    public void OnUnitDied(Unit unit)
    {
      if (unit is EnemyController)
      {
        Enemies.Remove(unit as EnemyController);
        if (Enemies.Count == 0)
        {
          Debug.Log("[플레이어 승리]");
          // ChangeState(new WinState(this, StateSystem));
        }
      }
      else if (unit is PlayerController)
      {
        Debug.Log("[적 승리]");
        // ChangeState(new LoseState(this, StateSystem));        
      }
    }

    public void UseCard(CardSO cardSO, Unit user, Unit target)
    {
      // 1. 사용 카드 효과 발동      
      switch (cardSO.CardEffectType)
      {
        case CardEffectType.DealDamage:
          target.Damaged(cardSO.EffectValue);
          break;
        case CardEffectType.GainBlock:
          user.GainBlock(cardSO.EffectValue);
          break;
      }
      Debug.Log($"[{user.name}_카드 사용]: {cardSO.CardName}");
    }

    public bool UseEnergy(int cost)
    {
      if (PlayerStat.Energy < cost)
      {
        return false;
      }
      PlayerStat.Energy -= cost;
      //Debug.Log($"남은 에너지: {PlayerStat.Energy}");
      return true;
    }

    public void ResetBlock<T>(T unit)
    {      
      if (unit is PlayerController)
      {
        PlayerStat.Block = 0;
      }
      else if (unit is EnemyController)
      {
        foreach (EnemyController enemy in Enemies)
        {
          enemy.Stat.Block = 0;
        }
      }
    }

    public void ResetEnergy<T>(T unit)
    {      
      if (unit is PlayerController)
      {
        PlayerStat.Energy = PlayerStat.MaxEnergy;
      }
      else if (unit is EnemyController)
      {
        foreach (EnemyController enemy in Enemies)
        {
          enemy.Stat.Energy = enemy.Stat.MaxEnergy;
        }
      }
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
        var randomIndex = random.Next(i, deck.Count);
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