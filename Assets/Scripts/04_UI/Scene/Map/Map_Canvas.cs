using System;
using UnityEngine;

namespace UIs.Map
{
  public class Map_Canvas : MonoBehaviour
  {
    public Transform nodeParent;

    private void Awake()
    {
      nodeParent ??= GameObject.Find("NodeParent").transform;
    }
  }
}