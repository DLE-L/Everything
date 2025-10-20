using System;
using System.Threading.Tasks;
using GamePlay.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Units
{
  public class UI_UnitHP : MonoBehaviour
  {
    private HorizontalLayoutGroup  _horizontalLayoutGroup;
    [SerializeField] private Unit _myUnit;

    private void Awake()
    {
      _horizontalLayoutGroup ??= GetComponent<HorizontalLayoutGroup>();
      _myUnit ??= GetComponentInParent<Unit>();
    }

    private async void Start()
    {
      try
      {
        await InitializeUnitHPBar();

        _horizontalLayoutGroup.enabled = false;
      }
      catch (Exception e)
      {
        Debug.LogWarning($"UI_UnitHP Error: {e.Message}");
      }
    }

    private async Task InitializeUnitHPBar()
    {
      
    }
  }
}