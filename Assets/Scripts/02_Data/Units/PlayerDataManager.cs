using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core;
using Data.Collectible.Card;
using Utils;
using UnityEngine;

namespace Data.Units
{
  public static class PlayerDataManager
  {
    public static PlayerRunData NewRunInitialize(int maxHp, Dictionary<CardSO, int> deck, int takeGold = 0)
    {
      return new PlayerRunData(maxHp, deck, takeGold);
    }
    public static async Task<PlayerAccountData> LoadAccountDataAsync()
    {
      return await SaveLoadManager.LoadPlayerData();
    }

    public static async Task<PlayerAccountData> NewAccountDefaultDataAsync(AccountSO accountSO)
    {
      var accountData = new PlayerAccountData
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
      
      await SaveLoadManager.SavePlayerDataAsync(accountData);
      //AssetLoader.ReleaseAsset("Deck_Account_Default");
      Debug.Log($"-Account Data New-");
      return accountData;
    }
  }
}