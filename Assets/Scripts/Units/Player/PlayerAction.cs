using Item;
using GameSystems;
using UnityEngine;

namespace Units.Player
{
  public class PlayerAction
  {
    private Player _player;
    private PlayerRunData _runData;

    public void Init(Player player, PlayerRunData runData)
    {
      _player = player;
      _runData = runData;
    }

    public void UpdateGold(int gold)
    {
      _runData.RunStateGold += gold;
      if (_runData.RunStateGold < 0)
      {
        _runData.RunStateGold = 0;
      }
      Debug.Log($"[{gold}골드 획득][보유골드: {_runData.RunStateGold}]");
    }
    public void UpdateHealth(int health)
    {
      _runData.Stat.HP += health;
      if (_runData.Stat.HP > _runData.Stat.MaxHP)
      {
        _runData.Stat.HP = _runData.Stat.MaxHP;
      }
      else if (_runData.Stat.HP <= 0)
      {
        _player.Die();
        Debug.Log($"[플레이어 사망]");  
        return;
      }
      Debug.Log($"[{health}체력 획득][현재체력: {_runData.Stat.HP}]");
    }
    public void AddRelic(RelicSO relic) 
    {
      _runData.Relics.Add(relic.Name);
      Debug.Log($"[유물 {relic.Name} 추가]");
    }
    public void AddCard(CardSO card)
    {
      Debug.Log($"카드 획득: {card.name}");
    }
    public void RemoveCard(CardSO card)
    {
      Debug.Log($"카드 제거: {card.name}");
    }
    public void UpgradeCard(CardSO card)
    {
      Debug.Log($"카드 강화: {card.name}");
    }
    public void AddPotion(string potion) //TODO: PotionSO로 변경
    {
      Debug.Log($"포션 획득: {potion}");
    }
    public void StartBattle()
    {

    }
  }
}