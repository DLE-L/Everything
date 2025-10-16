using System.Collections.Generic;
using System;
using Data.Act.Encounter;
using GamePlay.Battle;
using GamePlay.Units;
using Data.Collectible.Card;
using Data.Reward;

namespace Core.Event
{
  public static class BattleEvent
  {
    #region Combat Event
    public static event Action OnCombatStart;
    public static void RaiseCombatStart() => OnCombatStart?.Invoke();
    public static event Action OnCombatEnd;
    public static void RaiseCombatEnd() => OnCombatEnd?.Invoke();
    public static event Action OnPlayerWin;
    public static void RaisePlayerWin() => OnPlayerWin?.Invoke();
    public static event Action OnPlayerLose;
    public static void RaisePlayerLose() => OnPlayerLose?.Invoke();
    public static event Action<RewardSO> OnRewardPhaseStart;
    public static void  RaiseRewardPhaseStart(RewardSO reward) => OnRewardPhaseStart?.Invoke(reward);
    public static event Action<TurnOwner> OnTurnStart;
    public static void RaiseTurnStart(TurnOwner team) => OnTurnStart?.Invoke(team);
    public static event Action<List<Unit>> OnTurnEnd;
    public static void RaiseTurnEnd(List<Unit> team) => OnTurnEnd?.Invoke(team);
    #endregion

    #region Card Event
    public static event Action<List<CardSO>> OnHandUpdated;
    public static void RaiseHandUpdated(List<CardSO> hand) => OnHandUpdated?.Invoke(hand);
    public static event Action<CardSO> OnCardPlay;
    public static void RaiseCardPlay(CardSO card) => OnCardPlay?.Invoke(card);
    public static event Action<CardSO> OnCardDraw;
    public static void RaiseCardDraw(CardSO card) => OnCardDraw?.Invoke(card);
    public static event Action<CardSO> OnCardDiscard;
    public static void RaiseCardDiscard(CardSO card) => OnCardDiscard?.Invoke(card);
    public static event Action<CardSO> OnCardExhaust;
    public static void RaiseCardExhaust(CardSO card) => OnCardExhaust?.Invoke(card);
    public static event Action<CardSO> OnPlayPowerCard;
    public static void RaisePlayPowerCard(CardSO card) => OnPlayPowerCard?.Invoke(card);
    #endregion

    #region Battle Effect Event
    public static event Action<Unit, Unit, int> OnDealDamage;
    public static void RaiseDealDamage(Unit owner, Unit target, int damage) => OnDealDamage?.Invoke(owner, target, damage);
    public static event Action<Unit, Unit, int> OnTakeDamage;
    public static void RaiseTakeDamage(Unit owner, Unit target, int damage) => OnTakeDamage?.Invoke(owner, target, damage);
    public static event Action<Unit, int> OnGainBlock;
    public static void RaiseGainBlock(Unit owner, int block) => OnGainBlock?.Invoke(owner, block);
    public static event Action<Unit, int> OnHeal;
    public static void RaiseHeal(Unit owner, int heal) => OnHeal?.Invoke(owner, heal);
    public static event Action<Unit, Unit, int> OnApplyDebuff;
    public static void RaiseApplyDebuff(Unit owner, Unit target, int debuff) => OnApplyDebuff?.Invoke(owner, target, debuff);
    public static event Action<Unit, Unit, int> OnApplyBuff;
    public static void RaiseApplyBuff(Unit owner, Unit target, int buff) => OnApplyBuff?.Invoke(owner, target, buff);
    public static event Action<Unit> OnEnemyKill;
    public static void RaiseEnemyKill(Unit enemy) => OnEnemyKill?.Invoke(enemy);
    #endregion
  }
}

/*
OnCombatStart (전투 시작 시)
OnCombatEnd (전투 종료 시)
OnTurnStart (자신의 턴 시작 시)
OnTurnEnd (자신의 턴 종료 시)

카드 조작 관련 Hooks
OnCardPlay (카드를 사용할 때)
OnCardDraw (카드를 뽑을 때)
OnCardDiscard (카드를 버릴 때)
OnCardExhaust (카드를 소멸시킬 때)

전투 행위 관련 Hooks
OnDealDamage (피해를 줄 때)
OnTakeDamage (피해를 입을 때)
OnGainBlock (방어도를 얻을 때)
OnHeal (체력을 회복할 때)
OnApplyDebuff (적에게 디버프를 걸 때)
OnApplyBuff (자신에게 버프를 걸 때)
OnEnemyKill (적을 처치할 때)

자원 관련 Hooks
OnGainGold (골드를 얻을 때)
OnEnterShop (상점에 방문할 때)
OnRest (휴식할 때)
*/