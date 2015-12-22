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
                    if (qPrediction.HitChance <= HitChance.High)
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
                    if (ePrediction.HitChance <= HitChance.High)
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
                if (R.IsInRange(target) && target.IsValidTarget() && target.Health >= Damage.RDamage(target))
                {
                    var rPrediction = R.GetPrediction(target);
                    if (rPrediction.HitChance <= HitChance.High)
                    {
                        R.Cast(rPrediction.CastPosition);
                    }
                }
            }
        }
    }
}