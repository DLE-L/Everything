using System.Collections.Generic;
using UnityEngine;

namespace UIs.Battle
{
  public class Canvas_Scene_Battle : MonoBehaviour
  {
    [SerializeField] private List<RectTransform> _enemiesTr = new();
    public List<RectTransform> enemiesTransform => _enemiesTr;
    
  }
}