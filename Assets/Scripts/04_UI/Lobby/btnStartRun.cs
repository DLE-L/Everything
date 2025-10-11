using Core;
using UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _04_UI.Lobby
{
  public class btnStartRun : MonoBehaviour
  {
    private void OnClick(PointerEventData data)
    {
      GameSystem.Instance.Scene.LoadSceneMap();
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