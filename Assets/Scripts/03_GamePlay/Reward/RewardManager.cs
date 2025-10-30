using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Event;
using Data.Act.Encounter;
using Data.Collectible.Card;
using Data.Collectible.Relic;
using Data.Reward;
using UIs.Reward;
using UnityEngine;

namespace GamePlay.Reward
{
  public class RewardManager : MonoBehaviour
  {
    public RewardUIManager rewardUIManager;
    public RewardData rewardData {get; private set; }
    
    private readonly LinkedList<RewardCard> _rewardCards = new();

    private void Awake()
    {
      rewardUIManager ??= FindFirstObjectByType<RewardUIManager>();
    }

    public async Task Init()
    {
      var encounter = GameSystem.Instance.Run.CurrentEncounter as EncounterCombat;
      var strategy = encounter?.RewardStrategy;
      if (strategy is null)
      {
        Debug.LogError($"Reward strategy is null");
        return;
      }
      rewardData = await strategy.GenerateRewardAsync();
      rewardUIManager.Init(rewardData);
    }

    private void OnRewardStart(RewardStrategySO rewardStrategy)
    {
      rewardUIManager.ShowReward();
    }

    public bool UpdateRewardResult(RewardCard rewardCard)
    {
      bool result = false;
      if (_rewardCards.Contains(rewardCard))
      {
        _rewardCards.Remove(rewardCard);
        result = true;
      }
      else if (_rewardCards.Count < rewardData.SelectableCardCount)
      {
        _rewardCards.AddLast(rewardCard);
        result = true;
      }

      return result;
    }

    // public void UpdateRelicResult(RewardRelic rewardRelic)
    // {
    //   
    // }

    public RewardResult AcceptReward()
    {
      var result = new RewardResult()
      {
        Cards = new List<CardSO>(_rewardCards.Select(card => card.CardSo)),
        Relics = new List<RelicSO>(),
        Gold = rewardData.Gold,
      };
      
      return result;
    }
    
    
    private void OnEnable()
    {
      BattleEvent.OnRewardStart += OnRewardStart;
    }
    private void OnDisable()
    {
      BattleEvent.OnRewardStart -= OnRewardStart;
    }
  }
}