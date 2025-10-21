using System;
using System.Collections.Generic;
using Core.Event;
using GamePlay.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Units
{
  public class UI_UnitHP : MonoBehaviour
  {
    [SerializeField] private HorizontalLayoutGroup  _horizontalLayoutGroup;
    [SerializeField] private RectTransform _layoutRectTransform;
    [SerializeField] private Unit _myUnit;
    [SerializeField] private GameObject _hPBarPrefab;
    [SerializeField] private List<GameObject> _hPBars;
    private Queue<GameObject> _hPBarsOn;
    private Queue<GameObject> _hpBarsOff;
    

    private void Awake()
    {
      _horizontalLayoutGroup ??= GetComponent<HorizontalLayoutGroup>();
      _layoutRectTransform ??= GetComponent<RectTransform>();
      _myUnit ??= GetComponentInParent<Unit>();
      
      _hPBarsOn = new Queue<GameObject>(_hPBars);
      _hpBarsOff = new Queue<GameObject>();
    }
    public void InitializeUnitHPBar(Unit myUnit)
    {
      try
      {
        _myUnit = myUnit;

        UpdateHPBarUI(_myUnit, _myUnit.Stat.MaxHP);
      
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutRectTransform);
        _horizontalLayoutGroup.enabled = false;
      }
      catch (Exception e)
      {
        Debug.LogError($"InitializeUnitHPBar Error: {e}");
        throw;
      }

    }

    private void UpdateHPBarUI(Unit target, int amount)
    {
      if (target != _myUnit) return;

      var hpBarCount = _myUnit.Stat.HP / 25 + 1;
      
      // 회복
      while (_hPBarsOn.Count < hpBarCount)
      {
        if (_hpBarsOff.Count == 0) break;
        var obj = _hpBarsOff.Dequeue();
        obj.SetActive(true);
        _hPBarsOn.Enqueue(obj);
      }
      
      // 데미지
      while (_hPBarsOn.Count > hpBarCount)
      {
        var obj = _hPBarsOn.Dequeue();
        obj.SetActive(false);
        _hpBarsOff.Enqueue(obj);
      }
    }

    private void OnEnable()
    {
      BattleEvent.OnDamageFeedback += UpdateHPBarUI;
      BattleEvent.OnHeal += UpdateHPBarUI;
    }

    private void OnDisable()
    {
      BattleEvent.OnDamageFeedback -= UpdateHPBarUI;
      BattleEvent.OnHeal -= UpdateHPBarUI;
    }
  }
}