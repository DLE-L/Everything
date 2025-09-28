using Units.Enemy;
using UnityEngine;
using Units;
using Utils;

namespace GameSystems.Scene.Battle
{
  public class TurnPlayerState : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;
    private Unit _playerUnit;

    public TurnPlayerState(BattleManager manager, BattleFSM fsm)
    {
      _manager = manager;
      _fsm = fsm;
    }

    public void Enter()
    {
      // 1. 카드 5장 드로우, 에너지 회복
      _manager.DrawCard(5);
      _manager.ResetEnergy(_manager.Player);

      // 2. 타겟 설정
      _manager.CurrentUser = _manager.Player;
      _manager.CurrentTarget = _manager.Enemies[0];

      foreach (var user in _manager.CurrentTarget.StatusEffect)
      {
        user.Key.OnTurnStart(_manager.CurrentUser, _manager.CurrentTarget);
      }
    }

    public void Execute()
    {
      // 1. 플레이어 행동

      // 2. 연결된 버튼 클릭해 턴 종료

      // 공격할 적 선택
      if (Input.GetMouseButtonDown(0))
      {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
          EnemyController enemy = hit.collider.GetComponent<EnemyController>();
          if (enemy != null && _manager.CurrentTarget != enemy)
          {
            _manager.CurrentTarget = enemy;
            Debug.Log($"[Select Enemy]: {enemy.name}");
          }
        }
      }
    }

    public void Exit()
    {
      _manager.DiscardHandCardAll();
      _manager.ResetBlock(_manager.Enemies);
    }
  }
}