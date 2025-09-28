using Utils;
using Units;

namespace GameSystems.Scene.Battle
{
  public class TurnEndState : IBattleState
  {
    private BattleManager _manager;
    private BattleFSM _fsm;

    private Unit _turnOwner;

    public TurnEndState(BattleManager manager, BattleFSM fsm, Unit owner)
    {
      _manager = manager;
      _fsm = fsm;
      _turnOwner = owner;
    }
    
    public void Enter()
    {

    }


    public void Execute()
    {

    }

    public void Exit()
    {
      
    }
  }
}