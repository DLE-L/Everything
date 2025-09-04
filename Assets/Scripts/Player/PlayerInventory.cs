
using System.Collections.Generic;
using GameSystem;
using Utils;
using UnityEngine;

namespace Player
{
  public class PlayerInventory : MonoBehaviour
  {
    // Addressalbe Address : Data/Player/Inventory
    // Addressalbe Address : Data/Player/CardDeckList
    public Dictionary<CardData, int> Inventory = new(); // Dictionary<CardData, Count>    
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
      //string json = AssetLoader.LoadAssetAsync<TextAsset>("Data/Player/Inventory").text;

    }

    public void SaveInventory()
    {
      // TODO: 인벤토리 데이터 저장      
    }

    public CardDeckData GetCardDeckData()
    {
      return currentDeck;
    }


  }
}