using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class GameManager : MonoBehaviour
  {
    [SerializeField] private GameObject MapNodeBase;

    private MapGenerator _generator = new();
    private MapRenderer _renderer = new();
    private void Awake()
    {
      Init();
    }

    public void Init()
    {
      EncounterDatabase.LoadEncounterData();
      _generator.GenerateNode(MapNodeBase);
      _renderer.mapData = _generator.mapData;
    }
  }
}