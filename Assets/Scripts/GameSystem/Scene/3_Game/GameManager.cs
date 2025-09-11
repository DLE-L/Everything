
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class GameManager : MonoBehaviour
  {


    private void Awake()
    {
      Init();
    }

    public void Init()
    {
      EncounterDatabase.LoadEncounterData();  

    }
  }
}