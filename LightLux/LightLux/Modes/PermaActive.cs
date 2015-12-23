using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Drawing;
using System.Linq;

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
            foreach (
                var enemy in
                    ObjectManager.Get<AIHeroClient>()
                        .Where(t => t.IsEnemy)
                        .Where(t => Program._Player.GetAutoAttackRange() >= t.Distance(Program._Player))
                        .Where(t => t.IsValidTarget()))
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
            if (Damage.LuxE())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical, Player.Instance.Position);
                if (target.IsValidTarget() && target.IsEnemy && !target.IsDead)
                {
                    Program.DrawLog("Kastuje E dla " + target.ChampionName, Color.Blue);
                    E.Cast(target);
                }
            }
        }
    }
}