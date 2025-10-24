using System.Threading.Tasks;
using GamePlay.Map;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace UIs.Map
{
  public class MapUIManager : MonoBehaviour
  {
    [Header("Map UI Manager")]
    private GameObject _currentCanvasObject;

    public async Task ShowEncounter(AssetReference encounterRef, Node node)
    {
      _currentCanvasObject = await AssetLoader.InstantiateAsync(encounterRef);
      var uiCanvas = _currentCanvasObject.GetComponent<CanvasEncounterBase>();
      uiCanvas ??= _currentCanvasObject.AddComponent<CanvasEncounterBase>();
      await uiCanvas.SettingUIAsync(node);
      
      Debug.Log($"Show EncounterRef: {_currentCanvasObject.name}");
    }

    public void CloseCurrentCanvas()
    {
      //AssetLoader.ReleaseInstance(_currentCanvasObject);
      Debug.Log($"Close EncounterRef: {_currentCanvasObject.name}");
    }
  }
}