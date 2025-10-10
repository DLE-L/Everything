using Core.Event;
using UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Title
{
  public class btnContinueGame : MonoBehaviour
  {
    private void OnClick(PointerEventData data)
    {
      SystemEvent.RaiseSceneLoadStart("2_Lobby");
    }
    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }
    void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
    }
  }
}
