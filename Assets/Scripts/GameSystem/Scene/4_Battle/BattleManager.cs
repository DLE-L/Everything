using UnityEngine;
using System.Collections.Generic;
using GameSystems.Scene.Game;
using GameSystems.Scene.Battle.States;
using Utils;
using Item;
using System.Linq;
using Units.Player;
using Units.Enemy;
using Units;
using System;
using System.Threading.Tasks;

namespace GameSystems.Scene.Battle
{
  public class BattleManager : MonoBehaviour
  {
    public List<BattleCardData> DrawPile = new();
    public List<BattleCardData> DiscardPile = new();
    public List<BattleCardData> Hand = new();

    public Player Player => GameSystem.Instance.Player;
    public StatData PlayerStat => Player.Stat;
    public List<EnemyController> Enemies = new();

    public Unit CurrentUser { get; set; }
    public Unit CurrentTarget { get; set; }

    public BattleStateSystem StateSystem { get; private set; } = new();
    public System.Random random = new();
    public UI_Card_Battle[] battleCards;
    public List<UI_Card_Battle> HandCardObjects = new(); 

    [SerializeField] private GameObject enemyGameObject;
    [SerializeField] private Transform enemyTransform;

    public Action<Unit> OnEnemyClicked;
    private TaskCompletionSource<Unit> _currentTargetSelectionTask;


    private void Awake()
    {
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
      }

      // 4. 카드 이벤트 등록
      foreach (UI_Card_Battle battleCard in battleCards)
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

      HandlePlayerClick();
    }

    public void BattleEncounter()
    {
      // TODO: 인타운터 AssetReferenceT로 변경으로 인해 재구현 필요
      var encounter = EncounterDatabase.CurrentEncounter;
      int count = 0;
      foreach (var enemy in encounter.Enemies)
      {
        enemyTransform.position += new Vector3(0, count, 0);
        GameObject go = Instantiate(enemyGameObject, enemyTransform);
        EnemyController controller = go.GetComponent<EnemyController>();
        controller.EnemyData = new BattleEnemyData(enemy);
        go.name = enemy.name + count;
        controller.Init();
        Enemies.Add(controller);
        count++;
      }
    }

    public List<Unit> GetAllEnemies()
    {
      List<Unit> list = new();
      foreach (var enemy in Enemies)
      {
        list.Add(enemy);
      }
      return list;
    }

    public void EnemyNextCard()
    {
      for (int i = 0; i < Enemies.Count; i++)
      {
        int rand = random.Next(0, Enemies[i].EnemyData.AbilityCards.Count);
        CardSO card = Enemies[i].EnemyData.AbilityCards[rand];
        Debug.Log($"[{Enemies[i].name}_Next Card]:{card.name}");
      }
    }

    public void OnUnitDied(Unit unit)
    {
      if (unit is EnemyController)
      {
        Enemies.Remove(unit as EnemyController);
        if (Enemies.Count > 0)
        {
          CurrentTarget = Enemies[0];
          Debug.Log($"[Death Target: {unit.name}][New Target:{CurrentTarget.name}]");
        }

        if (Enemies.Count == 0)
        {
          Debug.Log("[플레이어 승리]");
          // ChangeState(new WinState(this, StateSystem));
        }
      }
      else if (unit is Player)
      {
        Debug.Log("[적 승리]");
        // ChangeState(new LoseState(this, StateSystem));        
      }
    }

    public void UseCard(CardSO cardSO, Unit user, Unit target)
    {
      // 1. 사용 카드 효과 발동
      foreach (var effect in cardSO.Effects)
      {
        //effect.UseCard(user);
      }
      //Debug.Log($"[{user.name}_카드 사용]: {cardSO.CardName}");
    }

        public Task<Unit> SelectTargetAsync()
    {
        // 1. 새로운 '약속 티켓'을 발행합니다.
        _currentTargetSelectionTask = new TaskCompletionSource<Unit>();
        
        // 여기에 "적을 선택하세요" 화살표 UI를 활성화하는 코드를 넣습니다.
        //TargetingArrow.Instance.Show();
        
        // 2. 약속 티켓(Task)을 즉시 반환합니다. 
        //    (호출한 쪽에서는 이 Task를 await하며 기다리게 됩니다)
        return _currentTargetSelectionTask.Task;
    }

    private void HandlePlayerClick()
    {
      // 타겟 선택 대기 상태가 아닐 때는 클릭을 무시
      if (_currentTargetSelectionTask == null || _currentTargetSelectionTask.Task.IsCompleted)
      {
        return;
      }

      if (Input.GetMouseButtonDown(0))
      {
        // ... Raycast 로직 ...
        RaycastHit2D hit = new();
        if (hit.collider != null)
        {
          Unit enemy = hit.collider.GetComponent<Unit>();
          if (enemy != null)
          {
            // 클릭된 적 정보를 이벤트로 방송 (다른 용도를 위해 남겨둘 수 있음)
            OnEnemyClicked?.Invoke(enemy);

            // 3. '약속 티켓'에 결과를 기록하여, await 하던 곳을 깨웁니다!
            _currentTargetSelectionTask.SetResult(enemy);

            // 화살표 UI 비활성화
            //TargetingArrow.Instance.Hide();
          }
        }
      }
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
      if (unit is Player)
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
      if (unit is Player)
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

    public void DiscardHandCardRandom(int amount)
    {
      int cardsToDiscardCount = Mathf.Min(amount, Hand.Count);

      var shuffledHand = Hand.OrderBy(card => random.Next()).ToList();
      var cardsToDiscard = shuffledHand.Take(cardsToDiscardCount).ToList();

      foreach (var cardData in cardsToDiscard)
      {
        DiscardHandCard(cardData);
      }

    }
    public void DiscardHandCard(BattleCardData cardData)
    {
      if (Hand.Remove(cardData))
      {
        DiscardPile.Add(cardData);
      }
      // 데이터 변경이 일어났으므로 UI 업데이트 호출
      //UpdateHandUI();
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
    public void CardUIUpdate(UI_Card_Battle card, bool active)
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
          if (DiscardPile.Count == 0)
          {
            Debug.Log("뽑을 카드가 더 이상 없습니다.");
            break; // 버린 덱에도 카드가 없으면 종료
          }
          ReshuffleDiscardIntoDrawPile();
        }

        // 1. 데이터만 처리: 뽑을 덱 맨 위 카드를 손으로 옮김
        BattleCardData drawnCardData = DrawPile[0];
        DrawPile.RemoveAt(0);
        Hand.Add(drawnCardData);
      }

      // 2. UI 업데이트는 별도의 메서드에 위임
      //UpdateHandUI();
    }

    private void ReshuffleDiscardIntoDrawPile()
    {
      DrawPile.AddRange(DiscardPile);
      DiscardPile.Clear();
      Shuffle(DrawPile);
      Debug.Log("버린 덱을 섞어 뽑을 덱을 만들었습니다.");
    }

    // --- UI 업데이트 ---
    // 이 메서드가 현재 Hand 데이터 리스트를 보고 화면을 그려주는 모든 책임을 가집니다.
    private void UpdateHandUI()
    {
      // 1. 현재 손에 있는 카드 수(Hand.Count)와 실제 UI 오브젝트 수(HandCardObjects.Count)를 맞춥니다.
      // ... 카드가 많아졌으면 새로 Instantiate 하고, 적어졌으면 SetActive(false) 하거나 파괴하는 로직 ...

      // 2. 각 UI 오브젝트에 올바른 카드 데이터를 연결합니다.
      for (int i = 0; i < HandCardObjects.Count; i++)
      {
        if (i < Hand.Count)
        {
          //HandCardObjects[i].Setup(Hand[i]); // BattleCard의 Setup 메서드 호출
          HandCardObjects[i].gameObject.SetActive(true);
        }
        else
        {
          HandCardObjects[i].gameObject.SetActive(false);
        }
      }
      // ... 카드 위치를 예쁘게 재정렬하는 로직 ...
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
      Dictionary<string, int> data = new(Player.RunData.Deck);
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