using Core;
using GamePlay.Units;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateTurnEnemy : IBattleState
  {
    private readonly BattleManager _manager;
    private readonly StateMachine _fsm;

    public StateTurnEnemy(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    public void Enter()
    {
      //Debug.Log($"-Enemy Turn-");

      foreach (var unit in _manager.UnitManager.EnemyTeam)
      {
        if (unit is not EnemyController enemy)
        {
          Debug.LogError($"Enemy is null");
          return;
        }

        _manager.CardManager.PlayCard(enemy.NextCard, enemy, _manager);
      }

      _fsm.ChangeState(new StateTurnEnd(_manager, _fsm, TurnOwner.EnemyTeam));
    }

    public void Execute()
    {

      // 3. 플레이어 턴으로 변경
      // _battleManager.ChangePlayerTurnState();
    }

    public void Exit()
    {

    }
  }
}