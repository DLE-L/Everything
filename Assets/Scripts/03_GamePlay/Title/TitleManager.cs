using Core;
using UnityEngine;

namespace GamePlay.Title
{
  public class TitleManager : MonoBehaviour
  {
    void Awake()
    {
      GameSystem.Instance.RegisterTitleManager(this);
    }

    void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterTitleManager();
      }
    }
  }
}