using System;
using UnityEngine;

namespace Utils
{
  [Serializable]
  public class StatData
  {
    public int Hp;
    public int MaxHp;
    public int Energy;
    public int MaxEnergy;
  }

  public interface IHealthSystem
  {
    public void Damaged(int damage);
    public void Heal(int heal);
    public bool IsDie();
  }

  public class RunData // TODO: 추후 DB구현시 필요
  {
    // 2인 플레이 데이터
    public PlayerRunState Player1;
    public PlayerRunState Player2;

    // 공통 진행 상황
    // 현재 위치
    // 맵 시드 등등...

  }

  public interface IBattleState
  {
    public void Enter();
    public void Execute();
    public void Exit();
  }
}