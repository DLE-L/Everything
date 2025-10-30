using Core.Event;
using Data.Reward;
using GamePlay.Reward;
using UIs.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs.Reward
{
  public class btnSelectReward : MonoBehaviour
  {
    private RewardManager  _rewardManager;
    [SerializeField] private bool _isActive;
    private void Awake()
    {
      _rewardManager ??= FindAnyObjectByType<RewardManager>();
    }

    private void OnClick(PointerEventData obj)
    {
      if (_isActive) return; 

      _isActive = true;
      var rewardResult = _rewardManager.AcceptReward();
      if (rewardResult?.Cards.Count < _rewardManager.rewardData.SelectableCardCount) //|| 
          //rewardResult?.Relics.Count < _rewardManager.rewardData.SelectableRelicCount)
      {
        return;
      }
      
      SystemEvent.RaiseGrantsReward(rewardResult);
      BattleEvent.RaiseBattleEnd();
    }
    
    private void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += OnClick;
    }

    private void OnDisable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction -= OnClick;
    }
  }
}