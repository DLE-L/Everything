using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Data.Collectible.Card;
using UnityEngine;
using Utils;

namespace UIs.Map
{
  public class Canvas_DeckCardList : MonoBehaviour
  {
    [SerializeField] private Transform _cardRoot;
    public List<RuntimeCard> DeckCards => RunSystem.Instance.PlayerData.Deck;
    public AddressableObjectPooler AddressableObjectPooler { get; private set; }

    private void Awake()
    {
      _cardRoot ??= transform.Find("CardRoot");
    }

    private async void Start()
    {
      try
      {
        AddressableObjectPooler = new AddressableObjectPooler(GameSystem.Instance.Map.AssetLoader.DeckCardRef, 10, true, _cardRoot);
        await UpdateCardListAsync();
      }
      catch (Exception e)
      {
        Debug.LogException(e);
      }
    }

    private async Task UpdateCardListAsync()
    {
      List<Task<GameObject>> tasks = new();
      for (int i = 0; i < DeckCards.Count; i++)
      {
        var task = AddressableObjectPooler.Get(_cardRoot);
        tasks.Add(task);
      }
      
      var  result = await Task.WhenAll(tasks);
      for (int i = 0; i < result.Length; i++)
      {
        var instance = result[i];
        var deckCard = instance.GetComponent<DeckCard>();
        deckCard.Setup(DeckCards[i]);
      }
    }

    private void OnDestroy()
    {
      AddressableObjectPooler.Cleanup();
    }
  }
}