using System.Threading.Tasks;
using UIs.UIManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utils;

namespace UIs.Title
{
  public class TitleUIManager : UIManagerBase
  {
    [Header("Title UI Manager")]
    public Image btnContinueGameImage;

    public override async Task InitCanvasSceneAsync()
    {
      await base.InitCanvasSceneAsync();
      btnContinueGameImage ??= canvasPrefab.GetComponentInChildren<btnContinueGame>().GetComponent<Image>();
    }
  }
}