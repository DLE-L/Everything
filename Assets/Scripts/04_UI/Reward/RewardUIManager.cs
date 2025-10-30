using System.Collections.Generic;
using UnityEngine;
using Data.Reward;

namespace UIs.Reward
{
  public class RewardUIManager : MonoBehaviour
  {
    public GameObject rewardUI;
    [SerializeField] private List<RewardCard> _rewardCards;

    private void Awake()
    {
      if (_rewardCards.Count < rewardUI.transform.childCount - 1)
      {
        _rewardCards.Clear();
        for (int i = 0; i < rewardUI.transform.childCount - 1; i++)
        {
          _rewardCards.Add(rewardUI.transform.GetChild(i).GetComponent<RewardCard>());
        }
      }
    }

    public void ShowReward()
    {
      rewardUI.SetActive(true);
    }

    public void Init(RewardData rewardData)
    {
      for (int index = 0; index < _rewardCards.Count; index++)
      {
        var rewardCard = _rewardCards[index];
        rewardCard.SetRewardCard(rewardData.CardsToPresent[index]);
      }

      //Debug.Log($"Reward Setting End");
      rewardUI.SetActive(false);
    }
  }
}