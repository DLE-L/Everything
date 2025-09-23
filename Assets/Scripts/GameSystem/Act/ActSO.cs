using GameSystems.Act.Encounter;
using UnityEngine;
using System.Threading.Tasks;
using Utils;

namespace GameSystems.Act
{
  [CreateAssetMenu(fileName = "Act", menuName = "MyMenu/Act")]
  public class ActSO : ScriptableObject
  {
    public EncounterPoolSO Encounter;
    public async Task LoadActDataAsync()
    {
      Encounter = await AssetLoader.LoadAssetAsync<EncounterPoolSO>("EncounterPool");

    }
  }
}
