using UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Title
{
  public class btnGameExit : MonoBehaviour
  {
    private void OnClick(PointerEventData data)
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else 
      Application.Quit();
#endif
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