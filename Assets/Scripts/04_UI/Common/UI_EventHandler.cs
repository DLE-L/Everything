using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Common
{
  public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
  {
    public Action<PointerEventData> OnClickAction = null;
    public Action<PointerEventData> OnDownAction = null;
    public Action<PointerEventData> OnUpAction = null;
    public Action<PointerEventData> OnEnterAction = null;
    public Action<PointerEventData> OnExitAction = null;
    
    public static UI_EventHandler Get(GameObject go)
    {
      UI_EventHandler handler = go.GetComponent<UI_EventHandler>();
      handler ??= go.AddComponent<UI_EventHandler>();
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
