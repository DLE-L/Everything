
using GameSystem.Utils;
using UnityEngine;

namespace GameSystem
{
  public class GameSystem : MonoBehaviour
  {
    public static GameSystem instance;

    private void Awake()
    {
      Init();
    }

    public void Init()
    {
      if (instance == null)
      {
        instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
    }

    void OnDestroy()
    {
      AssetLoader.ReleaseAllAsset();
    }
  }
}