using GameSystem;
using UnityEngine;

namespace Enemy
{
  public class EnemyStat : MonoBehaviour, IStatSystem
  {
    public int Hp => throw new System.NotImplementedException();

    public int MaxHp => throw new System.NotImplementedException();

    public int Energy => throw new System.NotImplementedException();

    public int MaxEnergy => throw new System.NotImplementedException();

    public void Damaged(int damage)
    {
      throw new System.NotImplementedException();
    }

    public void Heal(int heal)
    {
      throw new System.NotImplementedException();
    }

    public bool IsDie()
    {
      throw new System.NotImplementedException();
    }
  }

}