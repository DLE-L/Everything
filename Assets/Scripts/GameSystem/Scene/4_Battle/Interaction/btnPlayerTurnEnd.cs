using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Battle
{
  public class btnPlayerTurnEnd : InteractableBase
  {
    public BattleManager battleManager;

    void Start()
    {
      if (battleManager == null)
      {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
      }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      battleManager.ChangeEnemyTurnState();
    }
  }
}