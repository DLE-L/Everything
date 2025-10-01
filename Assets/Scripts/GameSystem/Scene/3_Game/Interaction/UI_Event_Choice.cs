using UnityEngine;
using TMPro;
using System;
using Utils;
using Units;

namespace GameSystems.Scene.Game
{
  public class UI_Event_Choice : MonoBehaviour
  {
    [SerializeField] private GameObject _choiceTextObj;
    private TextMeshProUGUI _choiceText;
    public event Action OnClickChoice;

    void Awake()
    {      
      _choiceText = _choiceTextObj.AddComponent<TextMeshProUGUI>();      
      _choiceText.enableAutoSizing = true;
      _choiceText.alignment = TextAlignmentOptions.Center | TextAlignmentOptions.Midline;
    }

    public void OnClick()
    {      

    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnClickChoice?.Invoke();
      };

      OnClickChoice += OnClick;
    }
    void OnDisable()
    {
      OnClickChoice -= OnClick;
    }

  }
}