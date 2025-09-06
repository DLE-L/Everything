
using UnityEngine;
using System.Collections.Generic;
using GameSystems.Scene.Battle.States;
using Player;

namespace GameSystems.Scene.Battle
{
  public class BattleManager : MonoBehaviour
  {
    public List<string> DrawPile = new();
    public List<string> DiscardPile = new();
    public List<string> Hand = new();
    public Stack<string> Deck = new();

    public PlayerController Player;// { get; private set; }
    public PlayerInventory Inventory;// { get; private set; }

    public BattleStateSystem StateSystem { get; private set; }
    

    private void Awake()
    {
      StateSystem = new BattleStateSystem();
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
      Inventory = Player.Inventory;
    }

    private void Start()
    {
      StateSystem.ChangeState(new StateSetup(this, StateSystem));
    }

    public void Update()
    {
      StateSystem.Execute();
    }
  }
}