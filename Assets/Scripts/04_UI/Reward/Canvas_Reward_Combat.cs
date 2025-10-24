using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Collectible;
using Data.Collectible.Card;
using Data.Collectible.Relic;
using Data.Reward;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace UIs.Reward
{
  public class Canvas_Reward_Combat : MonoBehaviour
  {
    [SerializeField] private Transform _viewRewardCardRoot;
    [SerializeField] private Transform _viewRewardRelicRoot;
    
    [SerializeField] private AssetReference _btnRewardCard;
    [SerializeField] private AssetReference _btnRewardRelic;

    [SerializeField] private TextMeshProUGUI _txtRewardGold;

    private readonly List<GameObject> _rewardPrefabs = new List<GameObject>();

    private RewardData _rewardData;
    private LinkedList<CardSO> _selectCards;
    private LinkedList<RelicSO> _selectRelics;
    
    public async Task SetRewardData(RewardStrategySO rewardStrategy)
    {
      _rewardData = await rewardStrategy.GenerateRewardAsync();
      foreach (var card in _rewardData.CardsToPresent)
      {
        GameObject cardGo = null; //await AssetLoader.InstantiateAsync(_btnRewardCard, _viewRewardCardRoot);
        cardGo.name = card.Name;
        var item = cardGo.GetComponent<RewardItem>();
        item.Init(card, this);
        _rewardPrefabs.Add(cardGo);
      }

      foreach (var relic in _rewardData.RelicsToPresent)
      {
        GameObject relicGo = null;//await AssetLoader.InstantiateAsync(_btnRewardRelic, _viewRewardRelicRoot);
        relicGo.name = relic.Name;
        var item = relicGo.GetComponent<RewardItem>();
        item.Init(relic, this);
        _rewardPrefabs.Add(relicGo);
      }
      
      _txtRewardGold.text = _rewardData.Gold.ToString();
    }

    public bool SelectionItem(CollectibleSO item, bool isSelected)
    {
      if (item is CardSO card)
      {
        return UpdateSelectionList(_selectCards, card, _rewardData.SelectableCardCount, isSelected);
      }
      else if (item is RelicSO relic)
      {
        return UpdateSelectionList(_selectRelics, relic, _rewardData.SelectableRelicCount, isSelected);
      }
      
      return false;
    }

    private bool UpdateSelectionList<T>(LinkedList<T> selectionList, T item, int maxCount, bool isSelected)
    {
      if (!isSelected) 
      {
        selectionList.Remove(item);
        return true;
      }
      
      if (selectionList.Count < maxCount)
      {
        selectionList.AddLast(item);
        return true;
      }

      return false;
    }

    public RewardResult CompleteSelection()
    {
      var rewardResult = new RewardResult()
      {
        Cards = new List<CardSO>(_selectCards),
        Relics = new List<RelicSO>(_selectRelics),
        Gold = _rewardData.Gold,
      };
      
      return rewardResult;
    }

    public bool IsCompleteSelection()
    {
      return _selectCards.Count == _rewardData.SelectableCardCount && _selectRelics.Count == _rewardData.SelectableRelicCount;
    }
  }
}