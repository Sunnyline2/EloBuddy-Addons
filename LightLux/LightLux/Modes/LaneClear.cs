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
                    if (Damage.LuxPassive(minion))
                        Orbwalker.ForcedTarget = minion;
                    Q.Cast(minion);
                    if (Damage.LuxPassive(minion))
                        Orbwalker.ForcedTarget = minion;
                    E.Cast(minion);
                }
            }

            foreach (var monster in EntityManager.MinionsAndMonsters.GetJungleMonsters(Program._Player.Position, 1000f))
            {
                if (monster.IsValidTarget())
                {
                    W.Cast();
                    if (Damage.LuxPassive(monster))
                        Orbwalker.ForcedTarget = monster;
                    E.Cast(monster);
                    if (Damage.LuxPassive(monster))
                        Orbwalker.ForcedTarget = monster;
                    Q.Cast(monster);
                }
            }
        }
    }
}