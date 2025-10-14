using System;
using Core;
using Data.Units;
using UI.Title;
using UnityEngine;
using Utils;

namespace GamePlay.Title
{
  public class TitleManager : MonoBehaviour
  {
    public TitleUIManager titleUIManager { get; private set; }
    void Awake()
    {
      GameSystem.Instance.RegisterTitleManager(this);
    }

    private async void Start()
    {
      try
      {
        var accountData = await SaveLoadManager.LoadPlayerData();
        if (accountData is null)
        {
          titleUIManager.btnContinueGameImage.raycastTarget = false;
          titleUIManager.btnContinueGameImage.color = Color.red; //TODO: 변경 예정
          accountData = await PlayerDataManager.NewAccountDataAsync();
        }
        GameSystem.Instance.PlayerAccountData = accountData;
      }
      catch (Exception e)
      {
        Debug.LogError($"TitleManager Error: {e.Message}");
      }
    }

    void OnDestroy()
    {
      if (GameSystem.Instance.Title is not null)
      {
        GameSystem.Instance.UnregisterTitleManager();
      }
    }
  }
}