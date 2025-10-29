using System.Collections.Generic;
using UnityEngine;

namespace UIs.Battle
{
  public class Battle_Canvas : MonoBehaviour
  {
    [SerializeField] private List<RectTransform> _enemiesTr = new();
    public List<RectTransform> EnemiesTransform => _enemiesTr;
    
    [SerializeField] private Transform _handTr;
    public Transform HandTr => _handTr;
    
  }
}