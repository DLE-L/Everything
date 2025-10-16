using System.Threading.Tasks;
using GamePlay.Map;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UIs.Map
{
  public abstract class CanvasEncounterBase : MonoBehaviour
  {
    public abstract Task SettingUIAsync(Node node);
  }
}