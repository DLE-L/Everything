using System;
using System.Threading.Tasks;
using Core.Event;
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

    public Map_Canvas mapCanvas;
    private Canvas _mapCanvasObject;

    private void Awake()
    {
      mapCanvas ??= FindFirstObjectByType<Map_Canvas>();
      _mapCanvasObject ??= mapCanvas.GetComponent<Canvas>();
    }

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

    private void OnEncounterEnter()
    {
      _mapCanvasObject.enabled = false;
    }

    private void OnEncounterExit()
    {
      _mapCanvasObject.enabled = true;
    }

    private void OnEnable()
    {
      SystemEvent.OnEncounterEnter += OnEncounterEnter;
      SystemEvent.OnEncounterExit += OnEncounterExit;
    }

    private void OnDisable()
    {
      SystemEvent.OnEncounterEnter -= OnEncounterEnter;
      SystemEvent.OnEncounterExit -= OnEncounterExit;
    }
  }
}