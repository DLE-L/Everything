using Data.Collectible.Card;
using Data.Units;
using GamePlay.Battle;

namespace GamePlay.Units
{
  public class Player : Unit
  {
    private static Player Instance;
    public PlayerRunData RunData { get; private set; }
    public DeckSO DeckSO;

    void Awake()
    {
      if (Instance is null)
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
      Team = TurnOwner.PlayerTeam;
    }

    public void DataSetting(PlayerRunData runData)
    {
      RunData = runData;
      Stat = RunData.Stat;
    }
  }
}
