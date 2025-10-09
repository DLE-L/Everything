using Core.Event;
using Data.Card;
using Data.Character;
using GamePlay.Battle;

namespace GamePlay.Character
{
  public class Player : Unit
  {
    public PlayerRunData RunData { get; set; }
    public DeckSO DeckSO;

    void Awake()
    {
      Initialize(TurnOwner.PlayerTeam);
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
