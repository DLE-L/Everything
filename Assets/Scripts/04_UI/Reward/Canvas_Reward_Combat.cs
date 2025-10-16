using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Event;
using Data.Act.Encounter;
using Data.Collectible;
using Data.Collectible.Card;
using Data.Collectible.Relic;
using Data.Reward;
using TMPro;
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

    private readonly List<GameObject> _rewardPrefabs = new();

    private RewardSO _rewardSO;
    private LinkedList<CardSO> _selectCards;
    private LinkedList<RelicSO> _selectRelics;
    
    public async Task SetRewardData(RewardSO reward)
    {
      _rewardSO = reward;
      foreach (var card in reward.Cards)
      {
        var cardGo = await AssetLoader.InstantiateAsync(_btnRewardCard, _viewRewardCardRoot);
        cardGo.name = card.Name;
        var item = cardGo.GetComponent<RewardItem>();
        item.Init(card, this);
        _rewardPrefabs.Add(cardGo);
      }

      foreach (var relic in reward.Relics)
      {
        var relicGo = await AssetLoader.InstantiateAsync(_btnRewardRelic, _viewRewardRelicRoot);
        relicGo.name = relic.Name;
        var item = relicGo.GetComponent<RewardItem>();
        item.Init(relic, this);
        _rewardPrefabs.Add(relicGo);
      }
      
      _txtRewardGold.text = reward.Gold.ToString();
    }

    public bool SelectionItem(CollectibleSO item, bool isSelected)
    {
      if (item is CardSO card)
      {
        return UpdateSelectionList(_selectCards, card, _rewardSO.SelectAbleCardCount, isSelected);
      }
      else if (item is RelicSO relic)
      {
        return UpdateSelectionList(_selectRelics, relic, _rewardSO.SelectAbleRelicCount, isSelected);
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
      else if (selectionList.Count < maxCount)
      {
        selectionList.AddLast(item);
        return true;
      }

      return false;
    }

    public RewardData CompleteSelection()
    {
      return new RewardData(_selectCards.ToList(),  _selectRelics.ToList(), _rewardSO.Gold);
    }

    public bool IsCompleteSelection()
    {
      return _selectCards.Count == _rewardSO.SelectAbleCardCount && _selectRelics.Count == _rewardSO.SelectAbleRelicCount;
    }

    public void ReleaseRef()
    {
      foreach(var rewardRef in _rewardPrefabs)
      {
        AssetLoader.ReleaseInstance(rewardRef);
      }
    }
  }
}