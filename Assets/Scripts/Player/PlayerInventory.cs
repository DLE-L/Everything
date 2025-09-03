
using System.Collections.Generic;
using Card;
using Card.Data;
using UnityEngine;

namespace Player
{
  public class PlayerInventory : MonoBehaviour
  {
    public Dictionary<string, int> Inventory = new(); // Dictionary<CardId, Count>    
    public CardDeckList cardDeckList;
    public CardDeckData currentDeck;

    public void Init()
    {
      // 1. PlayerInventory.json에서 모든 카드정보 가져옴
      // 2. PlayerCardDeckList.json에서 덱 리스트 정보 가져옴
      // 3. 
    }

    public void LoadInventory()
    {
      // TODO: Addressable을 이용해 인벤토리 데이터 가져옴

      // Inventroy.Add ---
    }

    public void SaveInventory()
    {
      // TODO: 인벤토리 데이터 저장
      // CardDeck 
    }

    public CardDeckData GetCardDeckData()
    {
      return currentDeck;
    }


  }
}