using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Map
{
  public class MapUIManager : MonoBehaviour
  {
    [SerializeField] private Canvas _cvGame;
    public AssetReference narrativeCanvasRef;
    public AssetReference shopCanvasRef;
    public AssetReference restCanvasRef;
    
    private GameObject _currentCanvasObject;
    
    public Canvas GameUI => _cvGame;

    void Start()
    {
      _cvGame.enabled = true;
    }
    
    public async Task ShowEncounter(AssetReference encounterRef)
    {
      _currentCanvasObject = await encounterRef.InstantiateAsync().Task;
      Debug.Log($"Show EncounterRef: {_currentCanvasObject.name}");
    }
    
    private void CloseCurrentCanvas()
    {
      Addressables.ReleaseInstance(_currentCanvasObject);
      Debug.Log($"Close EncounterRef: {_currentCanvasObject.name}");
    }
  }
}