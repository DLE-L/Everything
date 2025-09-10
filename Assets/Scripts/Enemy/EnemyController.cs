using GameSystems.Scene.Battle;
using UnityEngine;

namespace Enemy
{
  public class EnemyController : MonoBehaviour
  {
    private EnemyStat _stat = new();

    public EnemyStat EnemyStat => _stat;

    public BattleManager battleManager;

    void Awake()
    {
      battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    void Start()
    {
      _stat.Init(this);
    }
  }
}