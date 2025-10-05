using GameSystems;
using GameSystems.Scene.Battle;
using Item;
using Utils;

namespace Units
{
  public class RunPlayer : Unit
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

