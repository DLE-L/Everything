using System;
using UnityEngine;
using GameSystems.Scene.Game;

namespace GameSystems.Act
{
  public class Node : MonoBehaviour
  {
    private GameManager _gameManager;
    private NodeSO _nodeData;

    void Awake()
    {
      _gameManager = GameSystem.Instance.Game;
    }

    public void Setup(NodeSO nodeData)
    {
      _nodeData = nodeData;
      GetComponent<SpriteRenderer>().sprite = nodeData.EncounterType.Icon;
    }

    public void OnClick()
    {
      if (_nodeData != null)
      {
        _nodeData.ExecuteAction(this);
      }
    }
  }
}