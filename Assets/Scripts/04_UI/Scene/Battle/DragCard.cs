using System;
using System.Threading.Tasks;
using Core;
using Data.Collectible.Card;
using GamePlay.Battle;
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
    
    public RuntimeCard RuntimeCard => _buttonBattleCard.RuntimeCard;
    
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
      //Debug.Log("드래그 종료");
      _canvasGroup.blocksRaycasts = true;
      GameSystem.Instance.Battle.StopDraggingCard();

      ReturnToHandRoutine();
    }

    private void ReturnToHandRoutine()
    {
      _originalLayoutGroup = originalParent.GetComponent<LayoutGroup>();
      if (_originalLayoutGroup is not null)
      {
        _originalLayoutGroup.enabled = false;
      }
      
      transform.SetParent(originalParent);
      transform.SetSiblingIndex(_originalSiblingIndex);

      if (_originalLayoutGroup is not null)
      {
        _originalLayoutGroup.enabled = true;
      }
    }

    private void OnEnable()
    {
      originalParent = transform.parent;
      _canvasGroup.blocksRaycasts = true;
    }
  }
}