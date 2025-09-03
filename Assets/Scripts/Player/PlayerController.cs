using UnityEngine;

namespace Player
{
  public class PlayerController : MonoBehaviour
  {
    public PlayerStat playerStat;
    public PlayerUI playerUI;

    private void Awake()
    {
      playerStat.Init();
      playerUI.Init();
    }

    private void Start()
    {
      SetHp();
      SetEnergy();
    }

    private void Update()
    {

    }

    public void SetHp()
    {
      playerUI.SetHP_UI(playerStat.statData);
    }

    public void SetEnergy()
    {
      playerUI.SetEnergy_UI(playerStat.statData);
    }
  }
}


