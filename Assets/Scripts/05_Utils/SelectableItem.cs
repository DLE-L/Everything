using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
  public class SelectableItem<T> : MonoBehaviour, IPointerClickHandler
  {    
    private T _value;
    public static event Action<T> OnItemSelected;

    public void Initialize(T value)
    {
      _value = value;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
      OnItemSelected?.Invoke(_value);
    }
  }
}