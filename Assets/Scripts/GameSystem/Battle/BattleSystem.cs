
using UnityEngine;

namespace GameSystem.Battle
{
  public class BattleSystem : MonoBehaviour
  {
    public static BattleSystem instance;

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
  }
}