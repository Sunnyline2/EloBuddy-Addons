using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
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
                    Player.IssueOrder(GameObjectOrder.AttackUnit, enemy);
                    //    Orbwalker.ForcedTarget = enemy;
                }
            }

            if (Damage.LuxE())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Base>().Where(enemy => enemy.IsValidTarget() && enemy.IsEnemy && Vector3.Distance(Program.EObject.Position, enemy.Position) <= E.Width + 15))
                {
                    if (enemy.IsValid)
                    {
                        E2.Cast(enemy);
                        return;
                    }
                }
            }
        }
    }
}