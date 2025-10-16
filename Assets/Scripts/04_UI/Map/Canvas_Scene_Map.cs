using System;
using UnityEngine;

namespace UIs.Map
{
  public class Canvas_Scene_Map : MonoBehaviour
  {
    public Transform nodeParent;

    private void Awake()
    {
      nodeParent ??= GameObject.Find("NodeParent").transform;
    }
  }
}