using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
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
                if (Damage.LuxPassive(enemy) && enemy.IsInAutoAttackRange(enemy))
                {
                    Chat.Print("Focusuje " + enemy.ChampionName " z luxpassive");
                    Orbwalker.ForcedTarget = enemy;
                }
                else
                {
                    Orbwalker.ForcedTarget = null;
                }
            }
        }
    }
}