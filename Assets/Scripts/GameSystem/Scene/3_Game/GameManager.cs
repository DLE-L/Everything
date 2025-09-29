using UnityEngine;
using System;
using Utils;
using System.Threading.Tasks;
using GameSystems.Act;


namespace GameSystems.Scene.Game
{
  public class GameManager : MonoBehaviour
  {
    private MapGenerator _generator;
    private GameObject _mapNodeBase;
    private Event_UI event_UI;

    public event Action<Node> OnClickNode;

    public void Awake()
    {
      GameSystem.Instance.RegisterGameManager(this);
    }

    public void OnDestroy()
    {
      if (GameSystem.Instance != null)
      {
        GameSystem.Instance.UnregisterGameManager();
      }
    }

    public async Task InitAsync()
    {
      await EncounterDatabase.LoadEncounterDataAsync();
      // await EventDatabase.LoadEventDataAsync();
      // await NodeInfoDataBase.LoadNodeInfoDataAsync();
      _mapNodeBase = await AssetLoader.LoadAssetAsync<GameObject>("Node_Prefab");

      MapSetting();

      event_UI = GameObject.Find("Event_UI").GetComponent<Event_UI>();
      event_UI.Init();

    }

    private void MapSetting()
    {
      _generator = new();
      _generator.Init();
      _generator.GenerateMap(_mapNodeBase);
    }

    public void UpdateGameManger()
    {

    }
  }
}