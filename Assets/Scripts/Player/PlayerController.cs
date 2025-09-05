using UnityEngine;

namespace Player
{
  public class PlayerController : MonoBehaviour
  {
    private static PlayerStat _stat = new();
    private static PlayerInventory _inventory = new();

    public static PlayerStat Stat => _stat;    
    public static PlayerInventory Inventory => _inventory;


    private void Awake()
    {
      _stat.Init();
      _inventory.Init();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
  }
}


