using System;
using Core;
using Data.Act.Encounter;
using Data.Map;
using Data.Map.RestOption;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UIs.Common;
using UnityEngine.UI;

namespace UIs.Map
{
  public class btnRestOption : MonoBehaviour
  {
    [SerializeField] private Image _imgChoice;
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private TextMeshProUGUI _txtDescription;
    [SerializeField] private TextMeshProUGUI _txtResult;
    private RestOptionSO _restOption;

    void Awake()
    {
      _imgChoice ??= transform.Find("imgChoice").GetComponent<Image>();
      _txtName ??= transform.Find("txtName").GetComponent<TextMeshProUGUI>();
      _txtDescription ??= transform.Find("txtDescription").GetComponent<TextMeshProUGUI>();
      _txtResult ??= transform.Find("txtResult").GetComponent<TextMeshProUGUI>();
    }

    public void SetOption(RestOptionSO restOption)
    {
      _restOption = restOption;
      
      _imgChoice.sprite = restOption.Icon;
      _txtName.text = restOption.OptionName;
      _txtDescription.text = restOption.Description;
      if (_restOption is not RestOptionHeal restOptionHeal) return; 
      SetResult(restOptionHeal);
    }

    private void SetResult(RestOptionHeal heal)
    {
      var statData = RunSystem.Instance.PlayerData.Stat;
      var healAmount = statData.MaxHP * heal.healPercentage;
      var potentialHP = statData.HP + healAmount;
      var calculateData = Mathf.FloorToInt(Mathf.Clamp(potentialHP, statData.HP, statData.MaxHP));
      
      _txtResult.text = $"{statData.HP}/{statData.MaxHP} -> {calculateData}/{statData.MaxHP}";
    }

    private async void OnClick(PointerEventData data)
    {
      try
      {
        //TODO btnNarrativeChoice. Reward 필요
        
      }
      catch (Exception e)
      {
        Debug.LogWarning($"NarrativeChoice warning: {e.Message}");
      }
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