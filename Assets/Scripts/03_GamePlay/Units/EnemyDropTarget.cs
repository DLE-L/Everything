using System;
using GamePlay.Battle;
using UIs.Battle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay.Units
{
  public class EnemyDropTarget : MonoBehaviour, IDropHandler
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
      Debug.Log(gameObject.name + " 위에 카드 드롭됨");

      var draggedObject = eventData.pointerDrag;
      var card = draggedObject?.GetComponent<DragCard>();

      if (card is null || Enemy is null) return;

      _battleManager?.CardDroppedOnEnemy(card, Enemy);
    }
  }
}