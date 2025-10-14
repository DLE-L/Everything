using UnityEngine;
using GamePlay.Battle;
using Data.Collectible.Card;
using Data.Units;

namespace GamePlay.Units
{
  public class EnemyController : Unit
  {
    private BattleManager _battleManager;
    public BattleEnemyData EnemyData { get; private set; }
    private readonly System.Random random = new();

    void Awake()
    {
      Team = TurnOwner.EnemyTeam;
    }

    public void DataSetting(BattleEnemyData data, BattleManager manager)
    {
      _battleManager = manager;
      EnemyData = data;
      Stat = data.Stat;
    }

    protected override void HandleTurnStart(TurnOwner turnOwner)
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