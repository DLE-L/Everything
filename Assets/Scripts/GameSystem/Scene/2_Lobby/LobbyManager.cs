using System.Threading.Tasks;
using Units.Player;
using Utils;


namespace GameSystems.Scene.Lobby
{
  public class LobbyManager
  {
    public void Init()
    {
      
    }

    public void ContinueGame()
    {

    }

    public void NewGame()
    {

    }

    public async void NewGameStartAsync()
    {
      PlayerAccountData accountData = new();
      accountData.DefaultCardDeck();
      await JsonData.SavePlayerDataAsync(accountData);
      GameSystem.Instance.Player.Init(accountData);
    }

    public async void ContinueGameStartAsync()
    {
      GameSystem.Instance.Player.Init(await JsonData.LoadPlayerData());
    }
  }
}