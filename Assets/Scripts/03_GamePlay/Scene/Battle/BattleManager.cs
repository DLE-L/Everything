using UnityEngine;
using System;
using System.Threading.Tasks;
using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Target;
using GamePlay.Battle.State;
using GamePlay.Reward;
using GamePlay.Units;
using UIs.Units;
using UIs.Battle;

namespace GamePlay.Battle
{
  public class BattleManager : MonoBehaviour
  {
    public EncounterCombat currentCombat;
    private DragCard _currentDragCard;

    public StateMachine Fsm { get; private set; }
    public CardManager CardManager { get; private set; }
    public UnitManager UnitManager { get; private set; }
    public RewardManager RewardManager { get; private set; }
    public BattleUIManager UIManager { get; private set; }
    public BattleAssetLoader AssetLoader { get; private set; }

    public TurnOwner CurrentTurnOwner { get; private set; }

    private void Awake()
    {
      GameSystem.Instance.RegisterBattleManager(this);
      CardManager ??= FindAnyObjectByType<CardManager>();
      UnitManager ??= FindAnyObjectByType<UnitManager>();
      RewardManager ??= FindAnyObjectByType<RewardManager>();
      UIManager ??= FindAnyObjectByType<BattleUIManager>();
      AssetLoader ??= FindAnyObjectByType<BattleAssetLoader>();
    }

    private async void Start()
    {
      try
      {
        Fsm = new StateMachine();
        CardManager.Init();
        await AssetLoader.Init();
        await Task.Yield();
        UIManager.Init();
        await RewardManager.Init();

        // TODO: 적 의도 보여줌

        Fsm.ChangeState(new StateSetupBattle(this, Fsm, TurnOwner.PlayerTeam));
      }
      catch (Exception e)
      {
        Debug.LogWarning($"BattleManager Start warning: {e.Message}");
      }
    }

    public void Update()
    {
      Fsm.Execute();
    }

    public bool IsDraggingCard()
    {
      //Debug.Log($"{_currentDragCard is null}");
      return _currentDragCard is not null;
    }

    public void StartDraggingCard(DragCard card)
    {
      _currentDragCard = card;
      //Debug.Log("드래그 시작: " + card.RuntimeCard.Data.Name);
      // 선택 사항: 타겟팅 시각 효과 표시, 적 하이라이트 등
    }

    public void StopDraggingCard()
    {
      if (_currentDragCard is not null)
      {
        //Debug.Log("드래그 중지: " + _currentDragCard.RuntimeCard.Data.Name);
      }

      _currentDragCard = null;
      TargetHighlight.ClearAllHighlights();
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