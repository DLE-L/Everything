using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Map
{
  public class MapUIManager : MonoBehaviour
  {
    [SerializeField] private Canvas _cvGame;
    [SerializeField] private Canvas _cvNarrative;
    [SerializeField] private Canvas _cvShop;
    [SerializeField] private Canvas _cvRest;
    [SerializeField] private AssetReference _narrativeCanvasRef;
    [SerializeField] private AssetReference _shopCanvasRef;
    [SerializeField] private AssetReference _restCanvasRef;


    public Canvas GameUI => _cvGame;
    public Canvas NarrativeUI => _cvNarrative;
    public Canvas ShopUI => _cvShop;
    public Canvas RestUI => _cvRest;

    void Start()
    {
      _cvGame.enabled = true;
      _cvNarrative.enabled = false;
      _cvShop.enabled = false;
      _cvRest.enabled = false;
    }

    public void ShowNarrative()
    {

    }
  }
}