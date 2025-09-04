using GameSystem.Utils;
using UnityEngine;

namespace Player
{
  public class PlayerStat : MonoBehaviour, IStatSystem // MonoBehaviour 제거 예정
  {
    // Addressalbe Address : Data/Player/Stat
    public PlayerScriptableObject playerSO; // TODO: Addressable로 리소스 로드로 변경 필요

    [Header("Player Stat")]
    public StatData statData;

    public int Hp => statData.Hp;
    public int MaxHp => statData.MaxHp;
    public int Energy => statData.Energy;
    public int MaxEnergy => statData.MaxEnergy;

    public void Init()
    {
      LoadPlayerStat();
    }

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

    public void LoadPlayerStat()
    {
      // TODO: 플레이어 스탯 로드
    }

    public void SavePlayerStat()
    {
      // TODO: 플레이어 스탯 저장
    }

  }
}