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
            var aaTarget = TargetSelector.GetTarget(Program._Player.AttackRange, DamageType.Mixed);
            if (Damage.LuxPassive(aaTarget) && aaTarget.IsValidTarget())
            {
                Orbwalker.ForcedTarget = aaTarget;
            }
            if (!(E.IsOnCooldown && E.IsReady()))
            {
                E2.Cast();
            }
            //KS
            var rTarget = TargetSelector.GetTarget(R.Range, DamageType.Magical, Player.Instance.ServerPosition);
            if (rTarget.IsValidTarget() && R.MinimumHitChance == HitChance.Immobile &&
                R.MinimumHitChance == HitChance.Impossible)
            {
                R.Cast(rTarget);
            }
        }
    }
}