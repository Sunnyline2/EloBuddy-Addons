﻿using EloBuddy;
using EloBuddy.SDK;

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
        }
    }
}