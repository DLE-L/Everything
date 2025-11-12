using Data.Collectible.Card;
using GamePlay.Map;

namespace UIs.Map
{
  public class btnShopCard : UI_CardBase
  {
    private CardSprite _cardSprite;

    public void Setup(RuntimeCard card)
    {
      SetupCard_UI(card);
    }
  }
}