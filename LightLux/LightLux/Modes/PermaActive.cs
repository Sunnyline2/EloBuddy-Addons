using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Linq;
using Color = System.Drawing.Color;

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
            //Force aa && auto ignite
            foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(t => t.IsEnemy).Where(t => Program._Player.GetAutoAttackRange() >= t.Distance(Program._Player)).Where(t => t.IsValidTarget()))
            {
                if (Damage.LuxPassive(enemy))
                {
                    Program.DrawLog("Focus:" + enemy.ChampionName + " luxpassive", Color.NavajoWhite);
                    Orbwalker.ForcedTarget = enemy;
                }
                else
                {
                    Orbwalker.ForcedTarget = null;
                }
            }

            //Steal baron or drake
            if (Config.Modes.Misc.rSteal)
            {
                foreach (
                    var monster in
                        ObjectManager.Get<AIHeroClient>()
                            .Where(target => target.IsMonster)
                            .Where(target => R.IsInRange(target))
                            .Where(target => target.IsValidTarget())
                            .Where(target => target.Health < Damage.RDamage(target)))
                {
                    Chat.Print(monster);
                }
            }

            //E2 cast
            if (Program.EObject != null)
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Base>().Where(enemy => enemy.IsValidTarget() && enemy.IsEnemy && Vector3.Distance(Program.EObject.Position, enemy.Position) <= E.Width + 15))
                {
                    Program.DrawLog("Wybucham E w " + enemy.Name + " bije za:" + Damage.EDamage(enemy), Color.MediumVioletRed);
                    E2.Cast();
                }
            }
            //W cast
            foreach (var ally in ObjectManager.Get<Obj_AI_Base>().Where(ally => ally.IsValidTarget() && ally.IsAlly && !ally.IsMinion && !ally.IsMonster && Vector3.Distance(Player.Instance.Position, ally.Position) <= W.Width))
            {
                // if (ally.IsStunned || !ally.IsCharmed || !ally.IsFeared || ally.HealthPercent < 60 && ally.CountEnemiesInRange(700) > 0) bugged!!
                if (ally.IsMe && ally.HealthPercent < 80 && ally.CountEnemiesInRange(600) > 0)
                {
                    W.Cast(ally);
                    Program.DrawLog("Pomagam tarcza w " + ally.Name, Color.MediumVioletRed);
                }
                else if (ally.CountEnemiesInRange(600) > 0 && ally.HealthPercent < 50)
                {
                    W.Cast(ally);
                    Program.DrawLog("Pomagam sobie tarczą!", Color.MediumVioletRed);
                }
            }
        }
    }
}