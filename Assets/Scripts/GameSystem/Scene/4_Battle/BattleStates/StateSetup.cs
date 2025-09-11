
using System.Collections.Generic;
using Units.Player;
using Utils;

namespace GameSystems.Scene.Battle.States
{
  public class StateSetup : IBattleState
  {
    private BattleManager _battleManager;
    private BattleStateSystem _stateSystem;

    public StateSetup(BattleManager battleManager, BattleStateSystem stateSystem)
    {
      _battleManager = battleManager;
      _stateSystem = stateSystem;
    }

    public void Enter()
    {
      // 1. 플레이어 덱 로드
      _battleManager.GetPlayerDeck();
      // 2. 플레이어 덱 섞기
      _battleManager.Shuffle(_battleManager.DrawPile);
      // 3. Setup 상태 종료(플레이어 턴 상태로 변경)
      _battleManager.ChangePlayerTurnState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
      
    }
  }
}