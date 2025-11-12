using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Map
{
  public class btnDelete : MonoBehaviour
  {
    private void OnClick(PointerEventData obj)
    {
      
    }
    
    private void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }

    private void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick; 
    }
  }
}