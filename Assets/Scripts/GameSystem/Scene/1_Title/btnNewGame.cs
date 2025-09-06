
using UnityEngine.EventSystems;
using Utils;

namespace GameSystems.Scene.Title
{
  public class btnNewGame : InteractableBase
  {
    public async override void OnPointerClick(PointerEventData eventData)
    {
      GameSystem.Instance.LoadLobbyScene();
      // TODO: 새로운 게임 시작
      // 1. 플레이어 초기 덱 설정
      PlayerAccountData accountData = await JsonData.LoadPlayerData();
      accountData ??= new();
      accountData.DefaultCardDeck();
      await JsonData.SavePlayerDataAsync(accountData);
      GameSystem.Instance.PlayerData = accountData;
    }
  }
}

/*
CardID 규칙 : 'CardType'_'CardName' <- 카드 타입_카드 영문명
2. 플레이어 카드 인벤토리 (PlayerInventory.json)
보유 카드 목록:

Strike (타격) x 4       

Defend (수비) x 4

Bash (강타) x 1         

Survivor (생존 본능) x 1

3. 플레이어 카드 덱 (PlayerCardDeck.json)
기본 덱 구성:

Strike (타격) x 4

Defend (수비) x 4

Bash (강타) x 1

Survivor (생존 본능) x 1
*/
