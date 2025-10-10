using UnityEngine;
using GamePlay.Battle;
using Data.Card;
using Data.Units;

namespace GamePlay.Units
{
  public class EnemyController : Unit
  {
    public BattleManager battleManager { get; private set; }
    public BattleEnemyData EnemyData { get; private set; }
    private System.Random random = new();

    void Awake()
    {
      Initialize(TurnOwner.EnemyTeam);
    }

    public void DataSetting(BattleEnemyData data, BattleManager manager)
    {
      battleManager = manager;
      EnemyData = data;
      Stat = data.Stat;
    }

    public override void HandleTurnStart(TurnOwner turnOwner)
    {
      base.HandleTurnStart(turnOwner);
      if (turnOwner == Team)
      {
        EnemyNextIntent();
      }
    }


    public void EnemyNextIntent()
    {
      int rand = random.Next(0, EnemyData.AbilityCards.Count);
      CardSO card = EnemyData.AbilityCards[rand];
      Debug.Log($"[{name}_Next Card]:{card.name}");
    }
  }
}