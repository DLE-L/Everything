using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum NodeType
  {
    Battle, Event,
    Elite, Shop, Rest,
    Boss
  }

  [Serializable]
  public class NodeData
  {
    public NodeType Type;
    public Vector2 Pos;    
    public NodeData(NodeType type, Vector2 pos)
    {
      Type = type;
      Pos = pos;      
    }
  }

  [Serializable]
  public class StatData
  {
    public int Hp;
    public int MaxHp;
    public int Energy;
    public int MaxEnergy;
    public int Block;

    public StatData() { }
    public StatData(StatData stat)
    {
      Hp = stat.Hp;
      MaxHp = stat.MaxHp;
      Energy = stat.Energy;
      MaxEnergy = stat.MaxEnergy;
      Block = stat.Block;
    }
  }

  public interface IHealthSystem
  {
    public void Damaged(int damage);
    public void Heal(int heal);
    public void GainBlock(int block);
    public void Die();
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