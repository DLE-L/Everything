using System.Collections.Generic;
using Data.Collectible.Card;
using System;

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
    public Dictionary<string, Dictionary<string, int>> Decks = new(); // Dictionary<덱 이름, Dictionary<카드ID, 개수>>
  }
}