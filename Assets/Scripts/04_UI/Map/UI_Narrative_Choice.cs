using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Data.Reward;
using Data.Act.Encounter;
using Core.Event;
using UI.Common;

namespace UI.Map
{
  public class UI_Narrative_Choice : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _choiceText;
    private int _choiceNumbering;

    void Awake()
    {
      _choiceText ??= GetComponentInChildren<TextMeshProUGUI>();
    }
    

    private void OnClick(PointerEventData data)
    {
      
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