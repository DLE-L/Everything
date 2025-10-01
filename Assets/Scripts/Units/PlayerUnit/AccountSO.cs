using UnityEngine;
using System.Collections.Generic;
using Item;

namespace Units
{
  [CreateAssetMenu(fileName = "DefaultAccountData", menuName = "MyMenu/Unit/DefaultAccount")]
  public class AccountSO : ScriptableObject
  {
    // 유저 정보
    // public string PlayerID = ""; // TODO: 추후 DB구현시 필요
    // public string NickName = ""; // TODO: 추후 DB구현시 필요    

    // 성장 요소    
    public int Gold;
    public List<CardSO> UnlockedCards; // 해금된 카드 목록
    public List<RelicSO> UnlockedRelics; // 해금된 유물 목록
    public List<DeckSO> Decks; // 덱 목록
  }
}