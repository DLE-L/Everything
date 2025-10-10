using GamePlay.Battle.State;

namespace Core
{
  public class StateMachine
  {
    public IBattleState CurrentState { get; private set; }

    public void Execute()
    {
      CurrentState?.Execute();
    }

    public void ChangeState(IBattleState newState)
    {
      CurrentState?.Exit();
      newState?.Enter();
      CurrentState = newState;
      //UnityEngine.Debug.Log($"[현재 턴 상태: {CurrentState.ToString()}]");
    }
  }
}
/*
 [전투시작] - Setup, BattleStart
 1_1 Draw Pile 생성 및 셔플
 1_2 Draw Pile 에서 카드 5장 뽑아 Hand에 채움
 1_3 Energy 3채움
 1_4 적의 행동 ui보여줌

 [플레이어 턴] - PlayerTurnStart, PlayerTurn, PlayerTurnEnd
 2_1 에너지 소모해 Hand 카드 사용
 2_2 소모 카드는 Discard Pile로 이동
 2_3 턴 종료 누를시 턴 종료 

 [적 턴] - EnemyTurnStart, EnemyTurn, EnemyTurnEnd
 3_1 적의 행동
 3_2 모든 적의 행동 종료시, 새로운 행동 예정
 3_3 모든 행동시 플레이어 턴 시작

 [각 턴 시작 시]
 A. 버프/디버프 처리 - 버프/디버프 지속시간 1턴 감소, 효과 발동
 B. 에너지 충전 - 플레이어는 에너지 최대치 회복
 C. 카드 드로우 - 카드가 5장이 되도록 Draw Pile에서 카드 뽑음

 [전투 종료] - Win, Loose, BattleEnd
- 승리: 모든 적 HP 0 될시 승리, 보상(카드, 골드, 유믈 등) 획득
- 패배: 플레이어 HP 0 될시 패배, Lobby로 돌아감
*/