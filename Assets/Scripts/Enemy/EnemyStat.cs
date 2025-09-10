using Utils;
using UnityEngine;
using GameSystems;

namespace Enemy
{
  public class EnemyStat : IHealthSystem
  {
    public StatData Stat;
    public EnemyController enemyController;

    public void Init(EnemyController controller)
    {
      enemyController = controller;

      enemyController.battleManager.OnCardActionDealDamage += (card) =>
      {
        if (card.Data.CardType == CardType.Attack)
        {
          Damaged(card.Data.EffectValue);
        }
      };
    }

    public void Damaged(int damage)
    {
      Debug.Log($"적 피해: {damage}");
    }

    public void Heal(int heal)
    {
      
    }
    public void GainBlock(int block)
    {

    }

    public bool IsDie()
    {
      throw new System.NotImplementedException();
    }
  }

}