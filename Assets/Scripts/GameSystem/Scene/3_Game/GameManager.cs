using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Utils;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace GameSystems.Scene.Game
{
  public class GameManager
  {
    private MapGenerator _generator;
    private GameObject _mapNodeBase;
    private Event_UI event_UI;

    public event Action<Node> OnClickNode;

    public async Task InitAsync()
    {
      await EncounterDatabase.LoadEncounterDataAsync();
      await EventDatabase.LoadEventDataAsync();
      await NodeInfoDataBase.LoadNodeInfoDataAsync();
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
      ClickNode();
    }

    public void ClickNode()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
          Node node = hit.collider.GetComponent<Node>();
          if (node != null)
          {
            Debug.Log($"[Select Node]: {node.name}\n[Select NodeType]: {node.NodeType}");            
            OnClickNode?.Invoke(node);
          }
        }
      }
    }
  }
}