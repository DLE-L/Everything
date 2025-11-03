using System.Collections.Generic;
using System;
using System.Linq;
using Data.Collectible.Card;

namespace Data.Units
{  
  [Serializable]
  public class PlayerAccountData
  {
    // 유저 정보
    // public string PlayerID = ""; // TODO: 추후 DB구현시 필요
    // public string NickName = ""; // TODO: 추후 DB구현시 필요

    // 성장 요소    
    public int Gold;
    public HashSet<string> UnlockedCardIDs = new(); // 해금된 카드 ID 목록
    public HashSet<string> UnlockedRelicIDs = new(); // Dictionary<해금 요소 ID>
    public List<Dictionary<string, int>> DeckRecipes = new(); // Dictionary<덱 이름, Dictionary<카드ID, 개수>>

    public void ConvertRunDataToAccountData(PlayerRunData runData)
    {
      Gold += runData.SaveGold;
      foreach (var runCard in runData.Deck)
      {
        UnlockedCardIDs.Add(runCard.Data.name);
      }

      foreach (var relic in runData.Relics)
      {
        UnlockedRelicIDs.Add(relic.name);
      }
    }
  }
}