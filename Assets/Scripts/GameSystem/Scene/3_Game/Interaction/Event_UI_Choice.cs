using UnityEngine;
using TMPro;
using System;
using Utils;
using Units.Player;

namespace GameSystems.Scene.Game
{
  public class Event_UI_Choice : MonoBehaviour
  {
    [SerializeField] private GameObject _choiceTextObj;
    private TextMeshProUGUI _choiceText;
    public ChoiceButton choiceButton;
    public event Action OnClickChoice;

    void Awake()
    {      
      _choiceText = _choiceTextObj.AddComponent<TextMeshProUGUI>();      
      _choiceText.enableAutoSizing = true;
      _choiceText.alignment = TextAlignmentOptions.Center | TextAlignmentOptions.Midline;
    }

    public void OnClick()
    {
      PlayerAction player = GameSystem.Instance.Player.Action;
      for (int i = 0; i < choiceButton.EventResult.Count; i++)
      {
        
      }
    }

    public void SetChoice(ChoiceButton choice)
    {
      choiceButton = choice;
      _choiceText.text = choice.ChoiceText;
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