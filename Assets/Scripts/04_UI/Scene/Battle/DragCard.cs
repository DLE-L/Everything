using System;
using System.Collections;
using Core;
using Data.Collectible.Card;
using GamePlay.Battle;
using GamePlay.Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIs.Battle
{
  [RequireComponent(typeof(CanvasGroup))]
  public class DragCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
  {
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    public Transform originalParent;
    private LayoutGroup _originalLayoutGroup;
    private Canvas _rootCanvas;
    private int _originalSiblingIndex;
    public CardSO CardData => _buttonBattleCard.CardSO;
    
    private Button_BattleCard _buttonBattleCard;

    private void Awake()
    {
      _rectTransform ??= GetComponent<RectTransform>();
      _canvasGroup  ??= GetComponent<CanvasGroup>();
      _rootCanvas ??= GetComponentInParent<Canvas>();
      _buttonBattleCard ??= GetComponent<Button_BattleCard>();
      
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      var battleManager = GameSystem.Instance.Battle;
      if (battleManager.CurrentTurnOwner == TurnOwner.EnemyTeam) return;
      
      _originalSiblingIndex = transform.GetSiblingIndex();
      
      transform.SetParent(_rootCanvas.transform);
      transform.SetAsLastSibling();
      
      _canvasGroup.blocksRaycasts = false;
      
      battleManager.StartDraggingCard(this);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
      _rectTransform.anchoredPosition += eventData.delta / _rootCanvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
      Debug.Log("드래그 종료");
      _canvasGroup.blocksRaycasts = true;

      // 드래그를 멈췄다고 매니저에게 알림
      GameSystem.Instance.Battle.StopDraggingCard();
      bool isDropTarget = eventData?.pointerDrag.GetComponent<EnemyDropTarget>() is not null;

      if (isDropTarget) return;
      
      StartCoroutine(ReturnToHandRoutine());
    }
    
    public IEnumerator ReturnToHandRoutine()
    {
      // 2. 원래 부모의 LayoutGroup을 찾아서 저장하고 비활성화
      _originalLayoutGroup = originalParent.GetComponent<LayoutGroup>();
      if (_originalLayoutGroup is not null)
      {
        _originalLayoutGroup.enabled = false;
      }
      
      transform.SetParent(originalParent);
      transform.SetSiblingIndex(_originalSiblingIndex);
      
      yield return null;
      
      if (_originalLayoutGroup is not null)
      {
        _originalLayoutGroup.enabled = true;
      }
      Debug.Log("원래 위치로 복귀 완료");
    }

    private void OnEnable()
    {
      originalParent = transform.parent;
    }
  }
}