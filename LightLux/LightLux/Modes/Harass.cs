using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Drawing;

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
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical, Player.Instance.Position);
                if (target.IsValidTarget())
                {
                    var qPrediction = Q.GetPrediction(target);
                    if (qPrediction.HitChancePercent <= Program.hitchanceHarras && target != null)
                    {
                        Program.DrawLog("Harrasuje z Q w:" + target.ChampionName + " DMG:" + Damage.QDamage(target), Color.CadetBlue);
                        Q.Cast(qPrediction.CastPosition);
                    }
                }
            }
            if (Config.Modes.Harass.UseE && E.IsReady())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical, Player.Instance.Position);
                if (target.IsValidTarget())
                {
                    var ePrediction = E.GetPrediction(target);
                    if (ePrediction.HitChancePercent <= Program.hitchanceHarras && target != null)
                    {
                        Program.DrawLog("Harrasuje z E w:" + target.ChampionName + " DMG:" + Damage.QDamage(target), Color.CadetBlue);
                        E.Cast(ePrediction.CastPosition);
                    }
                }
            }
        }
    }
}