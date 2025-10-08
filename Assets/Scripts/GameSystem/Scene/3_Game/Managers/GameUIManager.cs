using System.Threading.Tasks;
using UnityEngine;

namespace GameSystems.Scene.Game
{
  public class GameUIManager : MonoBehaviour
  {
    [SerializeField] private Canvas _cvGame;
    [SerializeField] private Canvas _cvNarrative;
    [SerializeField] private Canvas _cvShop;
    [SerializeField] private Canvas _cvRest;

    public Canvas GameUI => _cvGame;
    public Canvas NarrativeUI => _cvNarrative;
    public Canvas ShopUI => _cvShop;
    public Canvas RestUI => _cvRest;

    void Awake()
    {
      _cvGame.enabled = true;
      _cvNarrative.enabled = false;
      _cvShop.enabled = false;
      _cvRest.enabled = false;
    }

    public async Task InitializeAsync()
    {
      
    }
  }
}