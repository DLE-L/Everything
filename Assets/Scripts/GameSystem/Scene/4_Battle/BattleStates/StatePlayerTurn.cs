
using Units.Enemy;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StatePlayerTurn : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StatePlayerTurn(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      // 1. 카드 5장 드로우, 에너지 회복
      _battleManager.DrawCard(5);
      _battleManager.ResetEnergy(_battleManager.Player);

      // 2. 타겟 설정
      _battleManager.CurrentUser = _battleManager.Player;
      _battleManager.CurrentTarget = _battleManager.Enemies[0];
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
          if (enemy != null && _battleManager.CurrentTarget != enemy)
          {
            _battleManager.CurrentTarget = enemy;
            Debug.Log($"[Select Enemy]: {enemy.name}");
          }
        }
      }
    }

    public void Exit()
    {
      _battleManager.DiscardHandCardAll();
      _battleManager.ResetBlock(_battleManager.Enemies);
    }
  }
}