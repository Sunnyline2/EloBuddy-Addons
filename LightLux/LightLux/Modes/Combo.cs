﻿using EloBuddy;
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
                if (Config.Modes.Combo.UseQ)
                {
                    if (Q.IsReady() && Q.IsLearned)
                    {
                        var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical, Player.Instance.Position);
                        if (target.IsValidTarget() && !target.IsZombie && target.IsEnemy)
                        {
                            var qPred = Q.GetPrediction(target);
                            if (qPred.HitChancePercent <= 80)
                            {
                                Q.Cast(qPred.CastPosition);
                            }
                        }
                    }
                }
                if (Config.Modes.Combo.UseE)
                {
                    if (E.IsReady() && E.IsLearned)
                    {
                        var target = TargetSelector.GetTarget(E.Range, DamageType.Magical, Player.Instance.Position);
                        if (target.IsValidTarget() && !target.IsZombie && target.IsEnemy)
                        {
                            var ePred = E.GetPrediction(target);
                            if (ePred.HitChancePercent <= 80)
                            {
                                E.Cast(ePred.CastPosition);
                            }
                        }
                    }
                }

                if (Config.Modes.Combo.UseW)
                {
                    if (W.IsReady() && W.IsLearned)
                    {
                        foreach (var ally in EntityManager.Heroes.Allies.ToArray())
                        {
                            if (ally.IsValid() & ally != null)
                            {
                                if (ally.IsInRange(Player.Instance, W.Range))
                                {
                                    var wPred = W.GetPrediction(ally);
                                    if (wPred.HitChancePercent < 80)
                                    {
                                        W.Cast(wPred.CastPosition);
                                    }
                                }
                            }
                        }
                    }
                }

                if (Config.Modes.Combo.UseR)
                {
                    if (R.IsReady() && R.IsLearned)
                    {
                        var target = TargetSelector.GetTarget(R.Range, DamageType.Magical, Player.Instance.Position);
                        if (target.IsValidTarget() && !target.IsZombie && target.IsEnemy && target.Health < Damage.RDamage(target))
                        {
                            var rPred = R.GetPrediction(target);
                            if (rPred.HitChancePercent <= 80)
                            {
                                R.Cast(rPred.CastPosition);
                            }
                        }
                    }
                }
            }
            catch (Exception stack)
            {
                Chat.Print(stack.Message);
            }
        }
    }
}