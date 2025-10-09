using Core.Event;
using GamePlay.Character;
using UnityEngine;

namespace GamePlay.Relic
{
  public class RelicManager : MonoBehaviour
  {
    void OnEnable()
    {
      BattleEvent.OnTakeDamage += HandleTakeDamage;      
    }

    public void HandleTakeDamage(Unit owner, Unit target, int damage)
    {

    }
    // public void HandleTurnStart(List<Unit> owner)
    // {

    // }

    void OnDestroy()
    {
      BattleEvent.OnTakeDamage -= HandleTakeDamage;
      //BattleEvent.OnTurnStart -= HandleTurnStart;
      // ...
      // GameEvent.OnShopEnter -= HandleShopEnter;
      // ...
    }

  }
}