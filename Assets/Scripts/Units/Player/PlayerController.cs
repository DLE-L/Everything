using System;
using UnityEngine;
using Utils;

namespace Units.Player
{
  public class PlayerController : Unit
  {
    private PlayerInventory _inventory = new();
    public PlayerInventory Inventory => _inventory;

    private void Awake()
    {
      _inventory.Init();
    }

    private void Start()
    {
      MayDelete();
    }

    public void MayDelete()
    {
      Stat.MaxHp = 80;
      Stat.MaxEnergy = 3;
      Stat.Hp = Stat.MaxHp;
      Stat.Energy = Stat.MaxEnergy;
    }
  }
}


