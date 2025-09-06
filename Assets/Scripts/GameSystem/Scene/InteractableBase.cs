using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystems.Scene
{
  /// <summary>
  /// 모든 상호작용 UI의 기반이 될 추상 클래스입니다.
  /// 공통적인 포인터 이벤트(눌림 효과 등)를 처리하고,
  /// 클릭 시 호출될 함수를 인스펙터에서 연결할 수 있도록 UnityEvent를 제공합니다.
  /// </summary>
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

