using Data.Target;
using GamePlay.Battle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Battle
{
  public class DropTargetPlayer : MonoBehaviour, IDropHandler
  {
    private BattleManager _battleManager;

    private void Awake()
    {
      _battleManager = FindFirstObjectByType<BattleManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
      var draggedObject = eventData.pointerDrag;
      var card = draggedObject?.GetComponent<DragCard>();
      
      if (card is null) return;
      
      var isSelfTarget = false;
      foreach (var effect in card.RuntimeCard.Data.Effects)
      {
        if (effect.Target is SelfTargeting)
        {
          isSelfTarget = true;
        }
      }

      if (!isSelfTarget) return; 
      
      _battleManager.UnitManager.PlayerUnit.CardUsedOnTarget(card, _battleManager.UnitManager.PlayerUnit);
    }
  }
}