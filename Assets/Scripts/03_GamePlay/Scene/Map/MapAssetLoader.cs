using System.Collections.Generic;
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
    [SerializeField] private AssetReference _generateDataRef;
    [SerializeField] private List<AssetReference> _actsRef;
    
    public AssetReference NodePrefabRef => _nodePrefabRef;
    public AssetReference ButtonNarrativeChoiceRef => _buttonNarrativeChoiceRef;
    public AssetReference NarrativeCanvasRef => _narrativeCanvasRef;
    public AssetReference ShopCanvasRef => _shopCanvasRef;
    public AssetReference RestCanvasRef => _restCanvasRef;
    public AssetReference GenerateDataRef => _generateDataRef;
    public List<AssetReference> ActsRef => _actsRef;
    
  }
}