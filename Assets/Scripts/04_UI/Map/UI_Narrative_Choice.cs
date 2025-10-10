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
    private RewardSO _reward;
    private RewardInfo _info;

    void Awake()
    {
      _choiceText ??= GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetChoice(RewardInfo info)
    {
      _info = info;
      _reward = info.Reward;
      _choiceText.text = info.Description;
    }

    private void OnClick(PointerEventData data)
    {
      SystemEvent.RaiseOnChoiceReward(_reward);
      Debug.Log($"[선택지 선택]");
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