
using UnityEngine.EventSystems;

namespace GameSystems.Scene.Game
{
  public class btnBattleEnter : InteractableBase
  {
    public override void OnPointerClick(PointerEventData eventData)
    {
      EncounterDatabase.CurrentEncounter = EncounterDatabase.encounters["Encounter_Goblin_Easy_01"];
      GameSystem.Instance.LoadBattleScene();      
    }
  }
}