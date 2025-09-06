using UnityEngine;

namespace Player
{
  public class PlayerController : MonoBehaviour
  {
    private PlayerStat _stat = new();
    private PlayerInventory _inventory = new();

    public PlayerStat Stat => _stat;    
    public PlayerInventory Inventory => _inventory;


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


