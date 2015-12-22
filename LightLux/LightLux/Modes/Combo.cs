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
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical, Player.Instance.Position);
                var qPrediction = Q.GetPrediction(target);
                if (qPrediction.HitChancePercent <= Program.hitchance && target != null)
                {
                    Q.Cast(qPrediction.CastPosition);
                }
            }

            if (E.IsReady() && Config.Modes.Combo.UseE)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical, Player.Instance.Position);
                if (E.IsInRange(target) && target.IsValidTarget())
                {
                    var ePrediction = E.GetPrediction(target);
                    if (ePrediction.HitChancePercent <= Program.hitchance && target != null)
                    {
                        E.Cast(ePrediction.CastPosition);
                    }
                }
            }

            if (W.IsReady() && Config.Modes.Combo.UseW)
            {
                foreach (var allies in EntityManager.Heroes.Allies)
                {
                    if (allies != null)
                    {
                        if (allies.IsCharmed || allies.IsAttackingPlayer || allies.IsFeared || allies.IsStunned ||
                            allies.IsTaunted || allies.IsValid)
                            W.Cast(allies);
                    }
                }
            }

            if (R.IsReady() && Config.Modes.Combo.UseR)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical, Player.Instance.ServerPosition);
                Chat.Print("R DMG: " + Damage.RDamage(target));
                if (R.IsInRange(target) && target.IsValidTarget() && target.Health <= Damage.RDamage(target))
                {
                    var rPrediction = R.GetPrediction(target);
                    if (rPrediction.HitChancePercent <= Program.hitchance)
                    {
                        R.Cast(rPrediction.CastPosition);
                    }
                }
            }
        }
    }
}