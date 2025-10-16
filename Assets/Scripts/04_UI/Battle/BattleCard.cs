using UnityEngine;
using TMPro;
using System;
using Data.Collectible.Card;

namespace UIs.Battle
{
  public class BattleCard : MonoBehaviour
  {
    // 카드가 클릭되었다는 사실을 외부(BattleManager)에 알리는 이벤트
    public event Action<BattleCard> OnClicked;

    [SerializeField] private TextMeshProUGUI _nameText;
    // ... 기타 UI 요소들 ...

    public CardSO CardData { get; private set; }

    public void Setup(CardSO data)
    {
      CardData = data;
      _nameText.text = CardData.Name;
      // ... UI 업데이트 ...
    }

    // Unity의 이벤트 시스템 (EventTrigger 등)을 통해 이 메서드가 호출되도록 설정
    public void OnPointerClick()
    {
      // "나 클릭됐어!" 라고 자신을 포함하여 이벤트를 방송
      OnClicked?.Invoke(this);
    }

  }
}