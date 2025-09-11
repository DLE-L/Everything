using GameSystems.Scene.Battle;
using UnityEngine;


namespace Units.Enemy
{
  public class EnemyController : Unit
  {
    public BattleManager battleManager;

    void Awake()
    {
      battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

      battleManager.OnCardActionDealDamage += (card) =>
      {
        if (card.Data.CardType == Utils.CardType.Attack)
        {
          Damaged(card.Data.EffectValue);
        }
      };
    }

    void Start()
    {

    }
  }
}