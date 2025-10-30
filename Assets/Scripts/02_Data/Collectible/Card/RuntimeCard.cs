using System;
using UnityEngine;

namespace Data.Collectible.Card
{
  [Serializable]
  public class RuntimeCard
  {
    public CardSO Data { get; private set; }
    public Guid InstanceID {get; private set;}

    public int CostModify { get; set; } = 0;
    public int DamagedModify { get; set; } = 0;

    public RuntimeCard(CardSO data)
    {
      Data = data;
      InstanceID = Guid.NewGuid();
    }

    public int GetCurrentCost()
    {
      return Mathf.Max(0, Data.Cost + CostModify);
    }

    public void UpgradeDamage(int amount)
    {
      DamagedModify += amount;
    }

    public void ReduceCost(int amount)
    {
      CostModify -= amount;
    }
  }
}