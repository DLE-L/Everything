using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePlay.Scene;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
    [SerializeField] private AssetReference _iconNodeRef;
    [SerializeField] private List<AssetReference> _actsRef;
    
    public AssetReference NodePrefabRef => _nodePrefabRef;
    public AssetReference ButtonNarrativeChoiceRef => _buttonNarrativeChoiceRef;
    public AssetReference NarrativeCanvasRef => _narrativeCanvasRef;
    public AssetReference ShopCanvasRef => _shopCanvasRef;
    public AssetReference RestCanvasRef => _restCanvasRef;
    public AssetReference GenerateDataRef => _generateDataRef;
    public AssetReference IconNodeRef => _iconNodeRef;
    public List<AssetReference> ActsRef => _actsRef;
    
    public NodeSprite NodeSprite {get; private set;}

    public async Task Init()
    {
      NodeSprite = await NodeSprite.CreateAsync(_iconNodeRef);
    }
  }
  
  public class NodeSprite
  {
    public Sprite Battle;
    public Sprite Elite;
    public Sprite Boss;
    public Sprite Narrative;
    public Sprite Rest;
    public Sprite Shop;

    public static async Task<NodeSprite> CreateAsync(AssetReference iconNodeRef)
    {
      var handle = iconNodeRef.LoadAssetAsync<Sprite[]>();
      await handle.Task;

      if (handle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"Node Sprite Sheet 로드 실패: {iconNodeRef.AssetGUID}");
        Addressables.Release(handle);
        return null;
      }

      Dictionary<string, Sprite> spriteMap;
      try
      {
        spriteMap = handle.Result.ToDictionary(sprite => sprite.name);
      }
      catch (System.ArgumentException ex)
      {
        Debug.LogError($"스프라이트 시트에 중복된 이름이 있습니다! {ex.Message}");
        Addressables.Release(handle);
        return null;
      }

      var nodeSprite = new NodeSprite();

      spriteMap.TryGetValue("Icon_Battle", out nodeSprite.Battle);
      spriteMap.TryGetValue("Icon_Elite", out nodeSprite.Elite);
      spriteMap.TryGetValue("Icon_Boss", out nodeSprite.Boss);
      spriteMap.TryGetValue("Icon_Narrative", out nodeSprite.Narrative);
      spriteMap.TryGetValue("Icon_Rest", out nodeSprite.Rest);
      spriteMap.TryGetValue("Icon_Shop", out nodeSprite.Shop);

      Addressables.Release(handle);

      return nodeSprite;
    }
  }
}