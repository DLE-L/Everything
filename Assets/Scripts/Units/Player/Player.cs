using GameSystems;
using Utils;

namespace Units.Player
{
  public class Player : Unit
  {
    public PlayerAccountData AccountData { get; private set; }
    public PlayerRunData RunData { get; private set; }
    public PlayerAction Action { get; private set; }

    public void Init(PlayerAccountData account)
    {
      AccountData = account;
      RunData = new(80);
      Action = new();
      Action.Init(this, RunData);
    }
  }
}

