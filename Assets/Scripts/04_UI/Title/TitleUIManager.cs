using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utils;

namespace UI.Title
{
  public class TitleUIManager : MonoBehaviour
  {
    [SerializeField] private AssetReference _uICanvasTitleRef;
    private GameObject _uiCanvasTitle;

    public Image btnContinueGameImage;
    private async void Start()
    {
      try
      {
        _uiCanvasTitle = await AssetLoader.InstantiateAsync(_uICanvasTitleRef);
      }
      catch (Exception e)
      {
        Debug.LogError($"TitleUIManager Error: {e.Message}");
      }
    }
    
    private void OnDestroy()
    {
      if (_uiCanvasTitle is not null)
      {
        AssetLoader.ReleaseInstance(_uiCanvasTitle);
      }
    }
  }
}