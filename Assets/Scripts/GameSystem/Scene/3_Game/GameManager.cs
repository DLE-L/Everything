using UnityEngine;
using System;
using Utils;
using GameSystems.Act;
using Item;

namespace GameSystems.Scene.Game
{
  public class GameManager : MonoBehaviour
  {
    private MapGenerator _generator;
    private GameObject _mapNodeBase;
    private MapGenerateSO _mapGenerateData;
    private Event_UI event_UI;

    public event Action<Node> OnClickNode;

    public void Awake()
    {
      GameSystem.Instance.RegisterGameManager(this);
      SystemEvent.RaiseOnStartNewRun();
      Debug.Log($"[New Run Start]");
    }

    public async void Start()
    {
      await CardDatabase.InitializeAsync();
      await EncounterDatabase.LoadEncounterDataAsync();
      //_mapNodeBase = await AssetLoader.LoadAssetAsync<GameObject>("Node_Prefab");
      _mapGenerateData = await AssetLoader.LoadAssetAsync<MapGenerateSO>("GenerateMap_Data");

      MapSetting();

      event_UI = GameObject.Find("Event_UI").GetComponent<Event_UI>();
      event_UI.Init();
    }

    private void MapSetting()
    {
      _generator = new();      
      _generator.GenerateMap(_mapNodeBase, _mapGenerateData);
    }

    public void UpdateGameManger()
    {

    }

    public void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterGameManager();
      }
    }
  }
}