using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core;
using Data.Collectible.Card;
using Utils;

namespace Data.Units
{
  public static class PlayerDataManager
  {
    public static PlayerRunData SetNewRunData(int maxHp, Dictionary<CardSO, int> deck, int takeGold = 0)
    {
      return new PlayerRunData(maxHp, deck, takeGold);
    }
    
    public static async Task SaveAccountData(PlayerRunData runData)
    {
      var accountData = GameSystem.Instance.PlayerAccountData;
      accountData.ConvertRunDataToAccountData(runData);
      await SaveLoadManager.SavePlayerDataAsync(accountData);
    }
    
    public static async Task<PlayerAccountData> LoadAccountDataAsync()
    {
      return await SaveLoadManager.LoadPlayerData();
    }

    public static async Task<PlayerAccountData> LoadDefaultAccountDataAsync(AccountSO accountSO)
    {
      var accountData = new PlayerAccountData
      {
        Gold = accountSO.Gold,
        UnlockedCardIDs = accountSO.UnlockedCards.Select(card => card.name).ToHashSet(),
        UnlockedRelicIDs = accountSO.UnlockedRelics.Select(relic => relic.name).ToHashSet(),
        DeckRecipes = accountSO.Decks
          .Select(deck => deck.Cards.ToDictionary(card => card.Card.name, card => card.Count))
          .ToList()
      };
      
      await SaveLoadManager.SavePlayerDataAsync(accountData);
      return accountData;
    }
  }
}