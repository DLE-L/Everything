using UnityEngine;

namespace Player
{
  public class PlayerController : MonoBehaviour
  {
    public PlayerStat playerStat;    
    public PlayerInventory playerInventory;

    private void Awake()
    {
      playerStat.Init();
      playerInventory.Init();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
  }
}


