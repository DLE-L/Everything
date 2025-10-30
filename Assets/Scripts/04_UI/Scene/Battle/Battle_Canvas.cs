using System.Collections.Generic;
using UnityEngine;

namespace UIs.Battle
{
  public class Battle_Canvas : MonoBehaviour
  {
    [SerializeField] private List<RectTransform> _enemiesTr = new();
    [SerializeField] private Transform _handTr;
    
    public List<RectTransform> EnemiesTransform => _enemiesTr;
    public Transform HandTr => _handTr;

  }
}