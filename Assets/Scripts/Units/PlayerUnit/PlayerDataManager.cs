using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Utils;
using UnityEngine;

namespace Units
{
  public static class PlayerDataManager
  {
    public static PlayerRunData RundInitialize(int maxHP, Dictionary<string, int> deck, int takeGold = 0)
    {
      return new PlayerRunData(maxHP, deck, takeGold);
    }

    public static async Task<PlayerAccountData> GetAccountDataAsync()
    {
      return await JsonData.LoadPlayerData();
    }

    public static async Task<PlayerAccountData> NewAccountDataAsync()
    {
      PlayerAccountData data = await DefaultToAccountData();
      await JsonData.SavePlayerDataAsync(data);
      AssetLoader.ReleaseAsset("Account_Default");
      return data;
    }

    private static async Task<PlayerAccountData> DefaultToAccountData()
    {
      var accountSO = await AssetLoader.LoadAssetAsync<AccountSO>("Account_Default");
      Debug.Log($"[Account Default Setting]");

      return new PlayerAccountData
      {
        Gold = accountSO.Gold,
        UnlockedCardIDs = accountSO.UnlockedCards.Select(card => card.name).ToHashSet(),
        UnlockedRelicIDs = accountSO.UnlockedRelics.Select(relic => relic.name).ToHashSet(),
        Decks = accountSO.Decks.ToDictionary(
          deck => deck.name,
          deck => deck.Cards.ToDictionary(
              cardCount => cardCount.Card.name,
              cardCount => cardCount.Count
        ))
      };
    }
  }
}