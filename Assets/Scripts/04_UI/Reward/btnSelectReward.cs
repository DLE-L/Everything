using System;
using UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Reward
{
  public class btnSelectReward : MonoBehaviour
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
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }
  }
}