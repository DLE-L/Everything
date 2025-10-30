using UnityEngine;
using Utils;

namespace GamePlay.Scene
{
  public abstract class SceneAssetLoader : MonoBehaviour
  {
    [SerializeField] protected string _myLabel;
    [SerializeField] protected string _nextSceneLabel;
    
    private void Start()
    {
      _ = AssetLoader.LoadAssetsByLabelAsync<Object>(_nextSceneLabel);
    }
    
    
    private void OnDestroy()
    {
      AssetLoader.SceneReleaseAll();
      AssetLoader.ReleaseAssetsByLabel(_myLabel);
    }
  }
}