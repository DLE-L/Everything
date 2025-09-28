using Units;
using Units.Enemy;
using Utils;
using Item;

namespace GameSystems.Scene.Battle
{
  public class TurnEnemyState : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;

    public TurnEnemyState(BattleManager manager, BattleFSM fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }
    public void Enter()
    {
      _manager.ResetEnergy(_manager.Enemies);

      for (int i = 0; i < _manager.Enemies.Count; i++)
      {
        EnemyUserCard(_manager.Enemies[i], _manager.CurrentTarget);
      }

      _manager.ChangePlayerTurnState();
    }

    public void Execute()
    {

      // 3. 플레이어 턴으로 변경
      // _battleManager.ChangePlayerTurnState();
    }
    public void Exit()
    {
      _manager.EnemyNextCard();
      _manager.ResetBlock(_manager.Player);
    }

    private void EnemyUserCard(EnemyController user, Unit target)
    {
      int rand = _manager.random.Next(0, user.EnemyData.AbilityCards.Count);
      CardSO card = user.EnemyData.AbilityCards[rand];
      _manager.UseCard(card, user, target);
    }
  }
}