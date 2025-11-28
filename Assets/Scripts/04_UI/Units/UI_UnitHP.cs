using System;
using Core.Event;
using GamePlay.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Units
{
  public class UI_UnitHP : MonoBehaviour
  {
    [SerializeField] private Image _imgHPBar;
    [SerializeField] private Unit _myUnit;

    private void Awake()
    {
      _imgHPBar ??= transform.Find("imgHPBar").GetComponent<Image>();
      _myUnit ??= GetComponentInParent<Unit>();
      
    }
    
    public void InitializeUnitHPBar(Unit myUnit)
    {
      _myUnit = myUnit;

      UpdateHPBarUI(_myUnit, _myUnit.Stat.MaxHP);
    }

    private void UpdateHPBarUI(Unit target, int loosHp)
    {
      if (target != _myUnit) return;

      if (_myUnit.Stat.MaxHP <= 0) return;
      
      _imgHPBar.fillAmount = (float)_myUnit.Stat.HP / _myUnit.Stat.MaxHP;
      Debug.Log($"Update HP: {(float)_myUnit.Stat.HP / _myUnit.Stat.MaxHP}");
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