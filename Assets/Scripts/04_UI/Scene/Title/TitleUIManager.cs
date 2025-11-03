using UnityEngine;
using UnityEngine.UI;

namespace UIs.Title
{
  public class TitleUIManager : MonoBehaviour
  {
    [Header("Title UI Manager")]
    public Image btnContinueGameImage;

    public void DisableContinueGameImage()
    {
      btnContinueGameImage.raycastTarget = false;
      btnContinueGameImage.color = Color.red;
    }
  }
}