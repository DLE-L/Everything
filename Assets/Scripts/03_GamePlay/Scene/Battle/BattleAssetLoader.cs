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
    public AssetReference EnemyPrefabRef => _enemyPrefabRef;
    public AssetReference BattleCardRef => _battleCardRef;

    

    public async Task Init()
    {
      
    }
  }
}