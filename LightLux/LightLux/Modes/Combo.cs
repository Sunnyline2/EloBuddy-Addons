using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System;

namespace LightLux.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Q.IsReady() && Config.Modes.Combo.UseQ)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (Q.IsInRange(target) && target.IsValidTarget() && Q.MinimumHitChance <= HitChance.Medium)
                {
                    Q.Cast(target);
                }
            }

            if (E.IsReady() && Config.Modes.Combo.UseE)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (E.IsInRange(target) && target.IsValidTarget() && E.MinimumHitChance <= HitChance.High)
                {
                    E.Cast(target);
                }
            }

            if (W.IsReady() && Config.Modes.Combo.UseW)
            {
                W.Cast();
            }

            if (R.IsReady() && Config.Modes.Combo.UseR)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (R.IsInRange(target) && target.IsValidTarget() && R.MinimumHitChance <= HitChance.High)
                {
                    R.Cast(target);
                }
            }
        }
    }
}