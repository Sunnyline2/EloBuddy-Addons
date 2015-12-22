using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace LightLux.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            // TODO: Add harass logic here
            if (Config.Modes.Harass.UseE && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target.IsValidTarget() && Q.MinimumHitChance <= HitChance.High)
                {
                    Q.Cast(target);
                }
            }
            if (Config.Modes.Harass.UseE && E.IsReady())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target.IsValidTarget() && E.MinimumHitChance <= HitChance.High)
                {
                    E.Cast(target);
                }
            }
        }
    }
}