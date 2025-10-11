using Core;
using Data.Card;
using GamePlay.Units;
using UnityEngine;

namespace GamePlay.Battle.State
{
  public class StateTurnEnemy : IBattleState
  {
    private BattleManager _manager;
    private StateMachine _fsm;

    public StateTurnEnemy(BattleManager manager, StateMachine fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    public void Enter()
    {
      Debug.Log($"[Enemy Turn State]");

      foreach (var unit in _manager.EnemyTeam)
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
      for (int i = 0; i < _manager.EnemyTeam.Count; i++)
      {
        var random = new System.Random();

        EnemyController enmey = _manager.EnemyTeam[i] as EnemyController;
        int rand = random.Next(0, enmey.EnemyData.AbilityCards.Count);
        CardSO card = enmey.EnemyData.AbilityCards[rand];
        UnityEngine.Debug.Log($"[{enmey.name}_Next Intent]:{card.name}");
      }
    }
  }
}