using Data.Collectible.Card;

namespace UIs.Map
{
  public class DeleteCard : UI_CardBase
  {
    public RuntimeCard Card { get; private set; }

    public void Setup(RuntimeCard card)
    {
      SetupCard_UI(card);
      Card = card;
    }
  }
}