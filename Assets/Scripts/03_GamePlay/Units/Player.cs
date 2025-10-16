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
  }
}
