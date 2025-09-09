
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

    public PlayerController Player { get; private set; }
    public PlayerInventory Inventory { get; private set; }

    public BattleStateSystem StateSystem { get; private set; }

    private System.Random _random = new();


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

    public void DiscardHandCard()
    {
      int handCount = Hand.Count;
      for (int i = 0; i < handCount; i++)
      {
        DiscardPile.Add(Hand[0]);
        Hand.RemoveAt(0);
      }
    }

    public void DrawCard(int amount)
    {
      for (int i = 0; i < amount; i++)
      {
        if (DrawPile.Count == 0)
        {
          DrawPile = DiscardPile;
          DiscardPile = new List<string>();
          Shuffle(DrawPile);
        }

        Hand.Add(DrawPile[0]);
        DrawPile.RemoveAt(0);
      }
    }

    public void Shuffle(List<string> deck)
    {
      for (int i = 0; i < deck.Count - 1; i++)
      {
        var randomIndex = _random.Next(i, deck.Count);
        (deck[i], deck[randomIndex]) = (deck[randomIndex], deck[i]);
      }
    }

    public void ChangePlayerStartState() => StateSystem.ChangeState(new StatePlayerStart(this, StateSystem));
    public void ChangePlayerTurnState() => StateSystem.ChangeState(new StatePlayerTurn(this, StateSystem));
    public void ChangePlayerEndState() => StateSystem.ChangeState(new StatePlayerEnd(this, StateSystem));

    public void ChangeEnemyStartState() => StateSystem.ChangeState(new StateEnemyStart(this, StateSystem));
    public void ChangeEnemyTurnState() => StateSystem.ChangeState(new StateEnemyTurn(this, StateSystem));  
    public void ChangeEnemyEndState() => StateSystem.ChangeState(new StateEnemyEnd(this, StateSystem));  
    
  }
}

/*
[전투 시작]
1. Setup
2. Player Turn
3. Enemy Turn

2~3반복
[전투 종료] - Win, Loose
3-1. Win : 게임씬으로 복귀
3-2. Loose : Lobby로 퇴장

*/