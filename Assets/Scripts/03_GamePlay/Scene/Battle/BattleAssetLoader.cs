using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GamePlay.Battle
{
  public class BattleAssetLoader : MonoBehaviour
  {
    [Header("Battle Asset Loader")]
    [SerializeField] private AssetReference _enemyPrefabRef;
    [SerializeField] private AssetReference _battleCardRef;
    [SerializeField] private AssetReference _imageCardRef;
    public AssetReference EnemyPrefabRef => _enemyPrefabRef;
    public AssetReference BattleCardRef => _battleCardRef;
    public AssetReference ImageCardRef => _imageCardRef;
    public CardSprite CardSprite { get; private set; }

    public async Task Init()
    {
      CardSprite = await CardSprite.CreateAsync(_imageCardRef);
    }
  }

  public class CardSprite
  {
    public Sprite Cost_0;
    public Sprite Cost_1; public Sprite Cost_2; public Sprite Cost_3;
    public Sprite Cost_4; public Sprite Cost_5; public Sprite Cost_6;
    public Sprite Cost_7; public Sprite Cost_8; public Sprite Cost_9;

    public Sprite Attack_Frame; public Sprite Power_Frame; public Sprite Skill_Frame;
    public Sprite Attack_Name; public Sprite Power_Name; public Sprite Skill_Name;

    public static async Task<CardSprite> CreateAsync(AssetReference imageCardRef)
    {
      var handle = imageCardRef.LoadAssetAsync<Sprite[]>();
      await handle.Task;

      if (handle.Status is not AsyncOperationStatus.Succeeded)
      {
        Debug.LogError($"Card Sprite Sheet 로드 실패: {imageCardRef.AssetGUID}");
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
      
      var cardSprite = new CardSprite();
      
      spriteMap.TryGetValue("Cost_0", out cardSprite.Cost_0);
      spriteMap.TryGetValue("Cost_1", out cardSprite.Cost_1);
      spriteMap.TryGetValue("Cost_2", out cardSprite.Cost_2);
      spriteMap.TryGetValue("Cost_3", out cardSprite.Cost_3);
      spriteMap.TryGetValue("Cost_4", out cardSprite.Cost_4);
      spriteMap.TryGetValue("Cost_5", out cardSprite.Cost_5);
      spriteMap.TryGetValue("Cost_6", out cardSprite.Cost_6);
      spriteMap.TryGetValue("Cost_7", out cardSprite.Cost_7);
      spriteMap.TryGetValue("Cost_8", out cardSprite.Cost_8);
      spriteMap.TryGetValue("Cost_9", out cardSprite.Cost_9);

      spriteMap.TryGetValue("Attack_Frame", out cardSprite.Attack_Frame);
      spriteMap.TryGetValue("Power_Frame", out cardSprite.Power_Frame);
      spriteMap.TryGetValue("Skill_Frame", out cardSprite.Skill_Frame);
      spriteMap.TryGetValue("Attack_Name", out cardSprite.Attack_Name);
      spriteMap.TryGetValue("Power_Name", out cardSprite.Power_Name);
      spriteMap.TryGetValue("Skill_Name", out cardSprite.Skill_Name);

      Addressables.Release(handle);
      
      return cardSprite;
    }
  }
}