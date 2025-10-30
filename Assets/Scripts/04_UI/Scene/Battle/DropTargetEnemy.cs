using Data.Target;
using GamePlay.Battle;
using GamePlay.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Battle
{
  public class DropTargetEnemy : MonoBehaviour, IDropHandler
  {
    public EnemyController Enemy { get; private set; }
    private BattleManager _battleManager;

    private void Awake()
    {
      Enemy = GetComponent<EnemyController>();
      _battleManager = FindFirstObjectByType<BattleManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
      //Debug.Log(gameObject.name + " 위에 카드 드롭됨");

      var draggedObject = eventData.pointerDrag;
      var card = draggedObject?.GetComponent<DragCard>();

      if (card is null || Enemy is null) return;
      
      var isSelfTarget = false;
      foreach (var effect in card.RuntimeCard.Data.Effects)
      {
        if (effect.Target is SelfTargeting)
        {
          isSelfTarget = true;
        }
      }

      if (isSelfTarget) return; 

      _battleManager?.UnitManager.PlayerUnit.CardUsedOnTarget(card, Enemy);
    }
  }
}