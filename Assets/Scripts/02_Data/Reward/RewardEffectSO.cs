using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Effect;
using Data.Target;
using GamePlay.Battle;
using GamePlay.Units;

namespace Data.Reward
{
  public class RewardEffectSO : RewardSO
  {
    public TargetingStrategySO Targeting;
    public GameEffectSO Effect;
    
    public override async Task GrantRewardAsync(Unit user, BattleManager manager)
    {
      TargetingContext context = new(
        user,
        manager.UnitManager.PlayerTeam,
        manager.UnitManager.EnemyTeam
        );

      List<Unit> targets = await Targeting.FindTargetsAsync(context);
      foreach (var target in targets)
      {
        Effect.Execute(user, target, manager);
      }
    }
  }
}