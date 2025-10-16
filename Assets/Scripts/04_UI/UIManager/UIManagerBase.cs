using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace UIs.UIManager
{
  public abstract class UIManagerBase : MonoBehaviour
  {
    [SerializeField] protected AssetReference canvasSceneRef;
    public GameObject canvasPrefab { get; private set; }

    public virtual async Task InitCanvasSceneAsync()
    {
      canvasPrefab = await AssetLoader.InstantiateAsync(canvasSceneRef);
    }
    
    private void OnDestroy()
    {
      if (canvasPrefab is not null)
      {
        AssetLoader.ReleaseInstance(canvasPrefab);
      }
    }
  }
}