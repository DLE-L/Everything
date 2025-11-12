using System.Collections.Generic;
using System;
using Data.Act.Encounter;
using GamePlay.Battle;
using GamePlay.Units;
using Data.Collectible.Card;
using Data.Reward;
using UIs.Battle;

namespace Core.Event
{
  public static class BattleEvent
  {
    #region Battle Event
    public static event Action OnBattleStart;
    public static void RaiseBattleStart() => OnBattleStart?.Invoke();
    public static event Action OnBattleEnd;
    public static void RaiseBattleEnd() => OnBattleEnd?.Invoke();
    public static event Action<RewardStrategySO> OnRewardStart;
    public static void RaiseRewardStart(RewardStrategySO rewardStrategy) => OnRewardStart?.Invoke(rewardStrategy);
    public static event Action<TurnOwner> OnTurnStart;
    public static void RaiseTurnStart(TurnOwner team) => OnTurnStart?.Invoke(team);
    public static event Action OnPlayerTurnStart;
    public static void RaisePlayerTurnStart() => OnPlayerTurnStart?.Invoke();
    public static event Action OnPlayerTurnEnd;
    public static void RaisePlayerTurnEnd() => OnPlayerTurnEnd?.Invoke();
    public static event Action OnEnemyTurnStart;
    public static void RaiseEnemyTurnStart() => OnEnemyTurnStart?.Invoke();
    public static event Action OnEnemyTurnEnd;
    public static void RaiseEnemyTurnEnd() => OnEnemyTurnEnd?.Invoke();
    #endregion

    #region Card Event
    public static event Action<RuntimeCard> OnCardPlay;
    public static void RaiseCardPlay(RuntimeCard card) => OnCardPlay?.Invoke(card);
    public static event Action<RuntimeCard, Unit> OnCardUsedOnTarget;
    public static void RaiseCardUsedOnTarget(RuntimeCard card, Unit target) => OnCardUsedOnTarget?.Invoke(card, target);
    public static event Action<RuntimeCard> OnCardDraw;
    public static void RaiseCardDraw(RuntimeCard card) => OnCardDraw?.Invoke(card);
    public static event Action<RuntimeCard> OnCardDiscard;
    public static void RaiseCardDiscard(RuntimeCard card) => OnCardDiscard?.Invoke(card);
    public static event Action<RuntimeCard> OnCardExhaust;
    public static void RaiseCardExhaust(RuntimeCard card) => OnCardExhaust?.Invoke(card);
    #endregion

    #region Battle Effect Event
    public static event Action<Unit, Unit, int> OnDealDamage;
    public static void RaiseDealDamage(Unit owner, Unit target, int damage) => OnDealDamage?.Invoke(owner, target, damage);
    public static event Action<Unit, Unit, int> OnTakeDamage;
    public static void RaiseTakeDamage(Unit owner, Unit target, int damage) => OnTakeDamage?.Invoke(owner, target, damage);
    public static event Action<Unit, int> OnDamageFeedback;
    public static void RaiseDamageFeedback(Unit target, int damage) => OnDamageFeedback?.Invoke(target, damage);
    public static event Action<Unit, int> OnGainBlock;
    public static void RaiseGainBlock(Unit owner, int block) => OnGainBlock?.Invoke(owner, block);
    public static event Action<Unit, int> OnHeal;
    public static void RaiseHeal(Unit owner, int heal) => OnHeal?.Invoke(owner, heal);
    public static event Action<Unit, Unit, int> OnApplyDebuff;
    public static void RaiseApplyDebuff(Unit owner, Unit target, int debuff) => OnApplyDebuff?.Invoke(owner, target, debuff);
    public static event Action<Unit, Unit, int> OnApplyBuff;
    public static void RaiseApplyBuff(Unit owner, Unit target, int buff) => OnApplyBuff?.Invoke(owner, target, buff);

    public static event Action<int> OnRequestDraw;
    public static void RaiseRequestDraw(int amount) => OnRequestDraw?.Invoke(amount);
    public static event Action<Unit,Unit, int> OnRequestDealDamage;
    public static void RaiseRequestDealDamage(Unit user, Unit target, int damage) => OnRequestDealDamage?.Invoke(user ,target, damage);
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