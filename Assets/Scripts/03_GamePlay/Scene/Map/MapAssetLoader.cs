using GamePlay.Scene;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Map
{
  public class MapAssetLoader : SceneAssetLoader
  {
    [Header("Map Asset Loader")] 
    [SerializeField] private AssetReference _nodePrefabRef;
    [SerializeField] private AssetReference _buttonNarrativeChoiceRef;
    [SerializeField] private AssetReference _narrativeCanvasRef;
    [SerializeField] private AssetReference _shopCanvasRef;
    [SerializeField] private AssetReference _restCanvasRef;
    
    public AssetReference nodePrefabRef => _nodePrefabRef;
    public AssetReference buttonNarrativeChoiceRef => _buttonNarrativeChoiceRef;
    public AssetReference narrativeCanvasRef => _narrativeCanvasRef;
    public AssetReference shopCanvasRef => _shopCanvasRef;
    public AssetReference restCanvasRef => _restCanvasRef;
    
  }
}