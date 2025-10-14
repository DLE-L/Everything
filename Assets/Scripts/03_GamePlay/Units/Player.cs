using Core.Event;
using Data.Collectible.Card;
using Data.Units;
using GamePlay.Battle;

namespace GamePlay.Units
{
  public class Player : Unit
  {
    public PlayerRunData RunData { get; set; }
    public DeckSO DeckSO;

    void Awake()
    {
      Team = TurnOwner.PlayerTeam;
    }

    public void DataSetting(PlayerRunData runData)
    {
      RunData = runData;
    }

    void OnEnable()
    {
      SystemEvent.OnStartNewRun += OnStartNewRun;
    }

    void OnDisable()
    {
      SystemEvent.OnStartNewRun -= OnStartNewRun;
    }

    public void OnStartNewRun()
    {
      //RunData = PlayerDataManager.RundInitialize(100, GameSystem.Instance.PlayerAccountData["Account_Default"]);
    }
  }
}
