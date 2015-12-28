using EloBuddy;
using EloBuddy.SDK;
using LightCassiopeia.MenuList;
using System.Linq;

namespace LightCassiopeia.Carry
{
    internal class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            //Focus poisoned targe
            //--------------------------------ULTIMATE-------------------------------------------
            if (MenuList.Combo.WithR && R.IsReady())
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                var rPred = R.GetPrediction(target);
                {
                    {
                        foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(enemy => enemy.Distance(Player.Instance.Position) <= R.Range))
                        {
                            if (enemy.CountEnemiesInRange(R.Range) >= MenuList.Combo.CountEnemiesInR && rPred.HitChancePercent >= MenuList.Misc.PredQ)
                            {
                                R.Cast(rPred.CastPosition);
                            }
                        }
                    }
                }
            }
            //-----------------------------------------------------------------------------------
            //--------------------------------Q--------------------------------------------------
            if (MenuList.Combo.WithQ && Q.IsReady())
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(en => en.IsValidTarget(Q.Range)))
                {
                    var qPred = Q.GetPrediction(enemy);
                    if (qPred.HitChancePercent >= Misc.PredQ)
                        Q.Cast(qPred.CastPosition);
                }
            //-----------------------------------------------------------------------------------
            //--------------------------------E--------------------------------------------------
            if (MenuList.Combo.WithE && E.IsReady())
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(en => en.IsValidTarget(E.Range)))
                {
                    if (enemy.HasBuffOfType(BuffType.Poison))
                        E.Cast(enemy);
                }
            //-----------------------------------------------------------------------------------
            //--------------------------------W--------------------------------------------------
            if (MenuList.Combo.WithW && W.IsReady())
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(en => en.IsValidTarget(W.Range)))
                {
                    if (enemy.HasBuffOfType(BuffType.Poison))
                        continue;
                    var wPred = SpellManager.W.GetPrediction(enemy);
                    if (wPred.HitChancePercent >= MenuList.Misc.PredQ)
                        SpellManager.W.Cast(wPred.CastPosition);
                }
            //-----------------------------------------------------------------------------------
        }
    }
}