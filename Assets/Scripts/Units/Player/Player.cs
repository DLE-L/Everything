using GameSystems;

namespace Units.Player
{
  public class Player : Unit
  {
    public PlayerAccountData AccountData { get; private set; }
    public PlayerRunData RunData { get; private set; }
    public PlayerAction Action { get; private set; }

    public void Init(PlayerAccountData account)
    {
      AccountInit(account);
      RunInit();
      ActionInit();
    }

    public void AccountInit(PlayerAccountData account) => AccountData = account;
    public void RunInit() => RunData = new(80);
    public void ActionInit()
    {
      Action = new();
      Action.Init(this, RunData);
    }
  }
}

