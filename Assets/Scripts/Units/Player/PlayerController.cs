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

    }
  }
}


