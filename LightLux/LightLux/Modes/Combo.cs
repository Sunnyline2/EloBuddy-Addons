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
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical, Player.Instance.ServerPosition);
                if (Q.IsInRange(target) && target.IsValidTarget())
                {
                    var qPrediction = Q.GetPrediction(target);
                    if (qPrediction.HitChance == HitChance.Immobile)
                    {
                        Q.Cast(qPrediction.CastPosition);
                    }
                }
            }

            if (E.IsReady() && Config.Modes.Combo.UseE)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical, Player.Instance.ServerPosition);
                if (E.IsInRange(target) && target.IsValidTarget())
                {
                    var ePrediction = E.GetPrediction(target);
                    if (ePrediction.HitChance == HitChance.Immobile)
                    {
                        E.Cast(ePrediction.CastPosition);
                    }
                }
            }

            if (W.IsReady() && Config.Modes.Combo.UseW)
            {
                W.Cast();
            }

            if (R.IsReady() && Config.Modes.Combo.UseR)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical, Player.Instance.ServerPosition);
                if (R.IsInRange(target) && target.IsValidTarget() && R.MinimumHitChance <= HitChance.High)
                {
                    var rPrediction = R.GetPrediction(target);
                    if (rPrediction.HitChance == HitChance.Immobile)
                    {
                        R.Cast(rPrediction.CastPosition);
                    }
                }
            }
        }
    }
}