using System.Diagnostics;
using Units;
using Units.Enemy;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StateEnemyTurn : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StateEnemyTurn(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      // 1. 에너지 회복
      _battleManager.ResetEnergy(_battleManager.Enemies);

      // 2. 타겟 설정
      _battleManager.CurrentTarget = _battleManager.Player;

      // 3. 적 행동
      for (int i = 0; i < _battleManager.Enemies.Count; i++)
      {
        EnemyUserCard(_battleManager.Enemies[i], _battleManager.CurrentTarget);
      }

      _battleManager.ChangePlayerTurnState();
    }

    public void Execute()
    {

      // 3. 플레이어 턴으로 변경
      // _battleManager.ChangePlayerTurnState();
    }

    public void Exit()
    {
      for (int i = 0; i < _battleManager.Enemies.Count; i++)
      {
        int rand = _battleManager.random.Next(0, _battleManager.Enemies[i].enemySO.AbilityCards.Count);
        CardSO card = _battleManager.Enemies[i].enemySO.AbilityCards[rand];
        UnityEngine.Debug.Log($"[{_battleManager.Enemies[i].name}_Next Card]:{card.name}");
      }
      _battleManager.ResetBlock(_battleManager.Enemies);
    }

    private void EnemyUserCard(EnemyController user, Unit target)
    {
      int rand = _battleManager.random.Next(0, user.enemySO.AbilityCards.Count);
      CardSO card = user.enemySO.AbilityCards[rand];
      _battleManager.UseCard(card, user, target);
    }
  }
}