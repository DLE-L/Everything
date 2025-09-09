using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.Scene
{
  public class InteractableBase : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
  {
    public virtual void OnPointerDown(PointerEventData eventData)
    {
      
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
      
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }
  }
}

