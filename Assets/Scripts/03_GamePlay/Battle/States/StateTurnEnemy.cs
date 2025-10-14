using Core;
using Data.Collectible.Card;
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
      Debug.Log($"[Enemy Turn State]");

      foreach (var unit in _manager.UnitManager.EnemyTeam)
      {
        // 카드 사용 로직
      }

      _fsm.ChangeState(new StateTurnEnd(_manager, _fsm, TurnOwner.PlayerTeam));
    }

    public void Execute()
    {

      // 3. 플레이어 턴으로 변경
      // _battleManager.ChangePlayerTurnState();
    }

    public void Exit()
    {
      EnemyNextIntent();
    }

    public void EnemyNextIntent()
    {
      for (int i = 0; i < _manager.UnitManager.EnemyTeam.Count; i++)
      {
        var random = new System.Random();

        EnemyController enemy = _manager.UnitManager.EnemyTeam[i] as EnemyController;
        if (enemy is null) return;
        
        int rand = random.Next(0, enemy.EnemyData.AbilityCards.Count);
        CardSO card = enemy.EnemyData.AbilityCards[rand];
        UnityEngine.Debug.Log($"[{enemy.name}_Next Intent]:{card.name}");
      }
    }
  }
}