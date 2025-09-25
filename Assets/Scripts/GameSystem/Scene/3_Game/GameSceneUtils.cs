using System.Collections.Generic;
using System.Threading.Tasks;
using GameSystems.Act.Encounter;
using UnityEngine;
using Utils;

namespace GameSystems.Scene.Game
{
  public static class EncounterDatabase
  {
    public static Dictionary<string, EncounterSO> encounters = new();
    public static EncounterSO CurrentEncounter;

    public async static Task LoadEncounterDataAsync()
    {
      var encounterList = await AssetLoader.LoadAssetLabelAsync<EncounterSO>("Encounter");
      foreach (var encounter in encounterList)
      {
        encounters.Add(encounter.name, encounter);
      }
    }

    public static void SetEncounterEnemy(string enemyID)
    {

    }
  }
  public static class EventDatabase
  {
    // public static Dictionary<string, IEvent> Events = new();
    // public static Dictionary<string, GameObject> EventUIs = new();
    // public static IEvent CurrentEvent;

    // public async static Task LoadEventDataAsync()
    // {
    //   var eventList = await AssetLoader.LoadAssetLabelAsync<GameObject>("Event");
    //   var eventUi = await AssetLoader.LoadAssetLabelAsync<GameObject>("Event_UI");
    //   foreach (var item in eventList)
    //   {
    //     item.GetComponent<IEvent>().Init();
    //     Events.Add(item.name, item.GetComponent<IEvent>());
    //   }

    //   foreach (var item in eventUi)
    //   {
    //     EventUIs.Add(item.name, item);
    //   }
    // }
  }

  public static class NodeInfoDataBase
  {
    // public static Dictionary<string, NodeInfo> Infos = new();
    // public static Dictionary<NodeType, NodeInfo> TypeInfos = new();

    // public async static Task LoadNodeInfoDataAsync()
    // {
    //   var infoSOs = await AssetLoader.LoadAssetLabelAsync<NodeInfoSO>("Node_Info");

    //   foreach (var info in infoSOs)
    //   {
    //     NodeInfo nodeInfo = new(info);
    //     Infos.Add(info.name, nodeInfo);
    //     TypeInfos.Add(nodeInfo.Type, nodeInfo);
    //   }
    // }

    // public static NodeInfo GetNodeInfo(NodeType type)
    // {
    //   return TypeInfos[type];
    // }
  }
}