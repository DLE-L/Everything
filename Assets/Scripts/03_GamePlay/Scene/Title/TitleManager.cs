using System;
using Core;
using Data.Units;
using GamePlay.Scene;
using UIs.Title;
using UnityEngine;
using Utils;

namespace GamePlay.Title
{
  public class TitleManager : MonoBehaviour
  {
    public TitleUIManager uiManager;
    public TitleAssetLoader assetLoader;
    void Awake()
    {
      GameSystem.Instance.RegisterTitleManager(this);
      
      uiManager ??= FindFirstObjectByType<TitleUIManager>();
      assetLoader ??= FindFirstObjectByType<TitleAssetLoader>();
    }

    private async void Start()
    {
      try
      {
        var accountData = await PlayerDataManager.LoadAccountDataAsync();
        if (accountData is null)
        {
          uiManager.btnContinueGameImage.raycastTarget = false;
          uiManager.btnContinueGameImage.color = Color.red; //TODO: 클릭 불가 & 회색처리
          var defaultAccountSo = await AssetLoader.LoadAssetAsync<AccountSO>(assetLoader.DefaultAccountSORef.AssetGUID);
          accountData = await PlayerDataManager.NewAccountDefaultDataAsync(defaultAccountSo);
        }
        GameSystem.Instance.PlayerAccountDataInitialize(accountData);
      }
      catch (Exception e)
      {
        Debug.LogError($"TitleManager Error => {e.Message}");
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