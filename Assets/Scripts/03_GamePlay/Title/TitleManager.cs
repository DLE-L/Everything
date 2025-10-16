using System;
using Core;
using Data.Units;
using UIs.Title;
using UnityEngine;
using Utils;

namespace GamePlay.Title
{
  public class TitleManager : MonoBehaviour
  {
    public TitleUIManager TitleUIManager;
    void Awake()
    {
      GameSystem.Instance.RegisterTitleManager(this);
      TitleUIManager ??= FindFirstObjectByType<TitleUIManager>();
    }

    private async void Start()
    {
      try
      {
        await TitleUIManager.InitCanvasSceneAsync();
        
        var accountData = await PlayerDataManager.LoadAccountDataAsync();
        if (accountData is null)
        {
          TitleUIManager.btnContinueGameImage.raycastTarget = false;
          TitleUIManager.btnContinueGameImage.color = Color.red; //TODO: 클릭 불가 & 회색처리
          accountData = await PlayerDataManager.NewAccountDataAsync();
        }
        GameSystem.Instance.PlayerAccountDataInitialize(accountData);
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