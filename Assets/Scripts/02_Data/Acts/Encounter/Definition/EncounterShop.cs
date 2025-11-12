using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Collectible.Card;
using Data.Rarity;
using GamePlay.Map;
using UnityEngine;

namespace Data.Act.Encounter
{
  [CreateAssetMenu(fileName = "Encounter_Shop_", menuName = "MyMenu/Act/Encounter/Shop")]
  public class EncounterShop : EncounterSO
  {
    [SerializeField] private int _cardCount;
    public List<RaritySO> Rarities;
    
    public List<RuntimeCard> CardList { get; private set; }
    
    public override async Task BeginAsync(MapManager mapManager, Node node)
    {
      System.Random random = new();
      var cardLoadingTasks = Rarities.Select(CardDatabase.GetCardsToRarityAsync);
      var results = await Task.WhenAll(cardLoadingTasks);
      
      CardList.AddRange(results.
        Select(result => new RuntimeCard(result.OrderBy(_ => random.Next()).First()))
      );
      
      await mapManager.uiManager.ShowEncounter(mapManager.AssetLoader.ShopCanvasRef, node);
    }
  }
}