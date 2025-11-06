using UnityEngine;
using System.Threading.Tasks;
using Data.Map;
using GamePlay.Map;

namespace Data.Act.Encounter
{
  public abstract class EncounterSO : ScriptableObject
  {
    public EncounterNodeStyleSO Style;
    public EncounterType Type;
    public int weight = 100;

    public abstract Task BeginAsync(MapManager mapManager, Node node);
  }

  public enum EncounterType
  {
    None,
    Narrative,
    Combat,
    Shop,
    Rest,
    Boss,
  }
}