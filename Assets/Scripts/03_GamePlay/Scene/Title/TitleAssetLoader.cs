using GamePlay.Scene;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GamePlay.Title
{
  public class TitleAssetLoader : SceneAssetLoader
  {
    [Header("Title Asset Loader")] 
    [SerializeField] private AssetReference _defaultAccountSORef;
    public AssetReference DefaultAccountSORef => _defaultAccountSORef;

  }
}