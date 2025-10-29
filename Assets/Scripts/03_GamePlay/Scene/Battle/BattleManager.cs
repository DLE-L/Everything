using UnityEngine;
using System.Linq;
using System;
using System.Threading.Tasks;
using Core;
using Core.Event;
using Data.Collectible.Card;
using Data.Act.Encounter;
using GamePlay.Battle.State;
using GamePlay.Units;
using UI.Units;
using UIs.Battle;
using StateMachine = Core.StateMachine;
using Unit = GamePlay.Units.Unit;

namespace GamePlay.Battle
{
  public class BattleManager : MonoBehaviour
  {
    private TaskCompletionSource<Unit> _currentTargetSelectionTask;
    private RaycastHit2D _hit;
    public EncounterCombat currentCombat;
    private DragCard _currentDragCard;

    public StateMachine Fsm { get; private set; }
    public CardManager CardManager { get; private set; }
    public UnitManager UnitManager { get; private set; }
    public BattleUIManager UIManager { get; private set; }
    public BattleAssetLoader AssetLoader { get; private set; }

    public TurnOwner CurrentTurnOwner { get; private set; }

    private void Awake()
    {
      GameSystem.Instance.RegisterBattleManager(this);
      UnitManager ??= FindAnyObjectByType<UnitManager>();
      UIManager ??= FindAnyObjectByType<BattleUIManager>();
      AssetLoader ??= FindAnyObjectByType<BattleAssetLoader>();
    }

    private async void Start()
    {
      try
      {
        Fsm = new StateMachine();
        UIManager.Init();
        await AssetLoader.Init();

        var DeckList = GameSystem.Instance.Run.PlayerRunData.Deck
          .SelectMany(deck => Enumerable.Repeat(deck.Key, deck.Value))
          .ToList();
        CardManager = new CardManager(DeckList);
        BattleEvent.OnPlayerTurnStart += CardManager.HandlePlayerTurnStart;

        // TODO: 적 의도 보여줌

        Fsm.ChangeState(new StateSetupBattle(this, Fsm, TurnOwner.PlayerTeam));
      }
      catch (Exception e)
      {
        Debug.LogError($"BattleManager Start warning: {e.Message}");
      }
    }

    public void Update()
    {
      Fsm.Execute();
    }

    public bool IsDraggingCard()
    {
      return _currentDragCard is not null;
    }

    public void StartDraggingCard(DragCard card)
    {
      _currentDragCard = card;
      Debug.Log("드래그 시작: " + card.CardData.name);
      // 선택 사항: 타겟팅 시각 효과 표시, 적 하이라이트 등
    }

    public void StopDraggingCard()
    {
      if (_currentDragCard is not null)
      {
        Debug.Log("드래그 중지: " + _currentDragCard.CardData.name);
      }

      _currentDragCard = null;
      EnemyTargetHighlight.ClearAllHighlights();
      // 선택 사항: 타겟팅 시각 효과 숨기기
    }

    // EnemyDropTarget이 카드 드롭 시 호출
    public void CardDroppedOnEnemy(DragCard card, EnemyController enemy)
    {
      // 드롭된 카드가 내가 추적하던 카드와 일치하는지 확인
      if (card == _currentDragCard)
      {
        Debug.Log($"{card.CardData.name} 카드를 {enemy.name} 위에 성공적으로 드롭!");

        // 1. 카드가 타겟을 필요로 하는지, 타겟이 유효한지 확인
        //    (예: if (card.CardData.RequiresTarget == TargetType.SingleEnemy))

        // 2. 적에게 카드 효과 적용
        //    (CardSO 구조에 따라 달라짐)
        //    예: enemy.TakeDamage(card.CardData.Damage);
        ApplyCardEffect(card.CardData, enemy);

        // 3. 사용된 카드 처리 (예: 버린 카드 더미로 이동, UI 오브젝트 파괴)
        //    중요: 카드 스스로가 아니라 매니저가 처리해야 함
        StartCoroutine(card.ReturnToHandRoutine());
        UIManager.AddressableObjectPooler.Release(card.gameObject); // 또는 파괴, 이동 등
        // 예: CardManager.MoveToDiscard(card.CardData);

        // 4. 드래그 상태 초기화 (StopDraggingCard가 OnEndDrag에서 호출되어 이미 처리됨)
      }
      else
      {
        Debug.LogWarning("드롭 감지됨, 하지만 추적 중인 카드와 일치하지 않음.");
        // 선택 사항: 이 경우 처리 (예: 카드를 손으로 되돌림)
        card.transform.SetParent(card.originalParent); // 예시: 원래 부모로 복귀
      }

      // 오류/불일치 시에도 드래그 상태는 확실히 리셋
      _currentDragCard = null;
    }

    // 카드 효과 적용 예시 함수
    private void ApplyCardEffect(CardSO cardData, EnemyController target)
    {
      Debug.Log($"{cardData.name} 효과를 {target.name}에게 적용");
      // cardData에 기반한 특정 카드 효과 로직을 여기에 작성
      // 예시:
      if (target != null)
      {
        // Unit 기본 클래스나 EnemyController에 TakeDamage 메소드가 있다고 가정
        // target.TakeDamage(cardData.GetDamageValue());
      }
    }

    public bool TryUseEnergy(int cardCost)
    {
      if (UnitManager.PlayerStat.Energy < cardCost)
      {
        return false;
      }

      UnitManager.PlayerStat.Energy -= cardCost;
      //Debug.Log($"남은 에너지: {PlayerStat.Energy}");
      return true;
    }

    private void UpdateTurnOwner(TurnOwner turnOwner)
    {
      CurrentTurnOwner = turnOwner;
    }

    private void OnEnable()
    {
      BattleEvent.OnTurnStart += UpdateTurnOwner;
    }

    private void OnDisable()
    {
      BattleEvent.OnPlayerTurnStart -= CardManager.HandlePlayerTurnStart;
      BattleEvent.OnTurnStart -= UpdateTurnOwner;
    }

    void OnDestroy()
    {
      if (GameSystem.Instance is not null)
      {
        GameSystem.Instance.UnregisterBattleManager();
      }
    }
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