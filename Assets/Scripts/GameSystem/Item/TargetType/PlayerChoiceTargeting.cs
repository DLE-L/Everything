using Units;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using GameSystems;

namespace Item
{
  [CreateAssetMenu(fileName = "Targeting_PlayerChoice", menuName = "MyMenu/Target/PlayerChoice")]
  public class PlayerChoiceTargeting : TargetingStrategySO
  {
    public override async Task<List<Unit>> FindTargetsAsync(Unit user, List<Unit> allAllies, List<Unit> allEnemies)
    {
      var tcs = new TaskCompletionSource<Unit>();
      Action<Unit> onTargetSelectedHandler = null;
      onTargetSelectedHandler = (selectedEnemy) =>
      {
        GameSystem.Instance.Battle.OnEnemyClicked -= onTargetSelectedHandler;        

        // '약속 티켓'에 선택된 적 정보를 기록하여 await을 깨웁니다.
        tcs.SetResult(selectedEnemy);
      };

      GameSystem.Instance.Battle.OnEnemyClicked += onTargetSelectedHandler;

      Debug.Log("[타겟 적 선택]");

      Unit target = await tcs.Task;

      return new List<Unit>() { target };
    }
  }
}