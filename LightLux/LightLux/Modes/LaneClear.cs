using EloBuddy.SDK;

namespace LightLux.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            // TODO: Add laneclear logic here
            foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy))
            {
                if (minion.IsValidTarget())
                {
                    if (Config.Modes.Misc.useQ)
                        Q.Cast(minion);
                    if (!(minion.IsValidTarget()))
                        return;
                    if (Config.Modes.Misc.useE)
                        E.Cast(minion);
                }
            }

            foreach (
                var monster in EntityManager.MinionsAndMonsters.GetJungleMonsters(Program._Player.Position, 1000))
            {
                if (monster.IsValidTarget())
                {
                    W.Cast(monster);
                    if (Config.Modes.Misc.useE)
                        E.Cast(monster);
                    if (!(monster.IsValidTarget()))
                        return;
                    if (Config.Modes.Misc.useQ)
                        Q.Cast(monster);
                }
            }
        }
    }
}