using Data.Collectible.Card;
using Utils;

namespace UIs.Map
{
  public class DeckCard : UI_CardBase, IPoolableObject
  {
    public RuntimeCard Card { get; private set; } 
    public void Setup(RuntimeCard card)
    {
      SetupCard_UI(card);
      this.Card = card;
    }

    public void ResetState()
    {
      Card = null;
    }
  }
}