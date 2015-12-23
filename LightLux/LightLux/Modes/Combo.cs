using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
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
            try
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
                        if (allies != null && allies.IsValid)
                        {
                            if (allies.IsCharmed || allies.IsAttackingPlayer || allies.IsFeared || allies.IsStunned ||
                                allies.IsTaunted || allies.IsValid)
                                W.Cast(allies);
                        }
                    }
                }

                if (R.IsReady() && Config.Modes.Combo.UseR)
                {
                    var target = TargetSelector.GetTarget(R.Range, DamageType.Magical, Player.Instance.Position);
                    if (R.IsInRange(target) && target.IsValidTarget() && target.Health <= Damage.RDamage(target))
                    {
                        var rPrediction = R.GetPrediction(target);
                        if (rPrediction.HitChancePercent <= Program.hitchance && target != null)
                        {
                            R.Cast(rPrediction.CastPosition);
                        }
                    }
                }
            }
            catch (Exception stack)
            {
                Chat.Print(stack.StackTrace);
            }
        }
    }
}