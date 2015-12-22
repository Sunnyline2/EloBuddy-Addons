using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace LightLux.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            // Forced aa
            //       var aaTarget = TargetSelector.GetTarget(Program._Player.AttackRange, DamageType.Mixed);
            //       if (Damage.LuxPassive(aaTarget) && aaTarget.IsValidTarget())
            //     {
            //          Orbwalker.ForcedTarget = aaTarget;
            //     }
            //       else
            //      {
            //           Orbwalker.ForcedTarget = null;
            //      }
        }
    }
}