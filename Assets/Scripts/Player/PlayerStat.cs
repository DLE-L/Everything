using Player.Data;
using UnityEngine;

namespace Player
{
  public class PlayerStat : MonoBehaviour // MonoBehaviour 제거 예정
  {    
    public PlayerScriptableObject playerSO; // TODO: Addressable로 리소스 로드로 변경 필요 

    [Space(10)]
    [Header("Player Stat")]
    public CurrentStat currentStat;

    public void Init()
    {
      LoadPlayerStat();
    }

    public void LoadPlayerStat()
    {
      if (playerSO.isUpdate == false)
      {
        currentStat.MaxHp = playerSO.BaseMaxHp;
        currentStat.MaxEnergy = playerSO.BaseMaxEnergy;
        currentStat.Hp = playerSO.BaseCurrentHp;
        currentStat.Energy = playerSO.BaseEnergy;

        playerSO.isUpdate = true;
      }
      else
      {
        currentStat.MaxHp = playerSO.UpdateMaxHp;
        currentStat.MaxEnergy = playerSO.UpdateMaxEnergy;
        currentStat.Hp = playerSO.UpdateCurrentHp;
        currentStat.Energy = playerSO.UpdateEnergy;
      }
    }

    public void SavePlayerStat()
    {
      playerSO.UpdateMaxHp      = currentStat.MaxHp;
      playerSO.UpdateMaxEnergy  = currentStat.MaxEnergy;
      playerSO.UpdateCurrentHp  = currentStat.Hp;
      playerSO.UpdateEnergy     = currentStat.Energy;
    }

    public void Damaged(int damage)
    {
      currentStat.Hp -= damage;
      if (currentStat.Hp <= 0)
      {
        Die();
      }
    }

    public void Heal(int heal)
    {
      currentStat.Hp += heal;
      if (currentStat.Hp > currentStat.MaxHp)
      {
        currentStat.Hp = currentStat.MaxHp;
      }
    }

    public void Die()
    {
      // TODO: Die
    }
  }
}