using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
  public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
  {
    public Action<PointerEventData> OnClickAction = null;
    public Action<PointerEventData> OnDownAction = null;
    public Action<PointerEventData> OnUpAction = null;
    public Action<PointerEventData> OnEnterAction = null;
    public Action<PointerEventData> OnExitAction = null;

    /// <summary>
    /// GameObject에 UI_EventHandler를 가져오거나, 없으면 새로 추가합니다.
    /// </summary>
    public static UI_EventHandler Get(GameObject go)
    {
      UI_EventHandler handler = go.GetComponent<UI_EventHandler>();
      if (handler == null)
        handler = go.AddComponent<UI_EventHandler>();
      return handler;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      OnClickAction?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      OnDownAction?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      OnUpAction?.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      OnEnterAction?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      OnExitAction?.Invoke(eventData);
    }
  }
}
