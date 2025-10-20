using System.Threading.Tasks;
using UIs.UIManager;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UIs.Battle
{
  public class BattleUIManager : UIManagerBase
  {
    [Header("Battle UI Manager")]
    [SerializeField] private AssetReference _battleCardRef;
    [SerializeField] private AssetReference _unitHPBarRef;
    
    public override async Task InitCanvasSceneAsync()
    {
      await base.InitCanvasSceneAsync();
    }
    
  }
}