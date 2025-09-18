using UnityEngine;
using Utils;

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
      _generator.Init();
      _renderer.MapData = _generator.GenerateMap(MapNodeBase);
    }

    void Update()
    {
       if (Input.GetMouseButtonDown(0))
      {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
          Node node = hit.collider.GetComponent<Node>();
          if (node != null )
          {            
            Debug.Log($"[Select Node]: {node.name}");
          }
        }
      }
    }
  }
}