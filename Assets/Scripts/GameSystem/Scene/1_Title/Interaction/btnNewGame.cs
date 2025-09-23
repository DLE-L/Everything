
using UnityEngine.EventSystems;
using UnityEngine;
using Utils;
using System;

namespace GameSystems.Scene.Title
{
  public class btnNewGame : MonoBehaviour
  {
    public event Action OnClickNewGame;

    public void OnClickNew()
    {
      GameSystem gameSystem = GameSystem.Instance;
      gameSystem.LoadLobbyScene();
      gameSystem.NewGameStartAsync();
    }

    void OnEnable()
    {
      UI_EventHandler.Get(gameObject).OnClickAction += (eventData) =>
      {
        OnClickNewGame?.Invoke();
      };

      OnClickNewGame += OnClickNew;
    }
    
    void OnDisable()
    {
      OnClickNewGame -= OnClickNew;
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
