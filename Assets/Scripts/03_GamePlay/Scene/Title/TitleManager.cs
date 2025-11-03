using System;
using System.Threading.Tasks;
using Core;
using Data.Units;
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

    private void Start()
    {
      var accountData = GameSystem.Instance.PlayerAccountData;
      if (accountData is not null) return;

      uiManager.DisableContinueGameImage();
    }

    public async Task SetNewAccountData()
    {
      var accountData = GameSystem.Instance.PlayerAccountData;
      if (accountData is not null)
      {
        // TODO TitleManager : if accountData가 존재할 시에 진짜 새로운 게임 할건지 물어보기
        Debug.LogWarning($"진짜 새게임 할건가요?");
      }
      var defaultAccountSo = await AssetLoader.LoadAssetReferenceAsync<AccountSO>(assetLoader.DefaultAccountSORef);
      accountData = await PlayerDataManager.LoadDefaultAccountDataAsync(defaultAccountSo);
      GameSystem.Instance.SetNewAccountData(accountData);
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