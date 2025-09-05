using Utils;
using UnityEngine;

namespace Player
{
  public class PlayerStat : IHealthSystem
  {
    public PlayerScriptableObject playerSO;
    public PlayerRunState runState = new();

    public void Init()
    {

    }

    public void Damaged(int damage)
    {
      
    }

    public void Heal(int heal)
    {
      
    }

    public bool IsDie()
    {
      return false;
    }
  }
}

/*
1. 플레이어 스탯 (PlayerStat.json)
가장 기본적인 생존 능력치야.

MaxHp (최대 체력): 80

Hp (현재 체력): 80

MaxEnergy (최대 에너지): 3

Energy (현재 에너지): 3

Gold (골드): 0
*/