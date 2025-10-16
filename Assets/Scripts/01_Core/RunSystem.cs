using Data.Units;
using GamePlay.Units;
using UnityEngine;

namespace Core
{
  public class RunSystem
  {
    public PlayerRunData PlayerRunData { get; private set; }
    public Player Player;

    public RunSystem(PlayerRunData data)
    {
      PlayerRunData = data;
      
    }
    public void Init()
    {
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
  }
}